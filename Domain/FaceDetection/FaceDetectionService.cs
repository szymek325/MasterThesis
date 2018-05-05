using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Repositories.Interface;
using Domain.Configuration;
using Domain.FaceDetection.DTO;
using Domain.Files;
using Microsoft.Extensions.Logging;

namespace Domain.FaceDetection
{
    public class FaceDetectionService : IFaceDetectionService
    {
        private readonly IFaceDetectionRepository detectionRepository;
        private readonly IFilesDomainService filesService;
        private readonly IMapper mapper;
        private readonly IGuidProvider guid;
        private readonly ILogger<FaceDetectionService> logger;

        public FaceDetectionService(IFaceDetectionRepository detectionRepository, IFilesDomainService filesService, IMapper mapper,
            IGuidProvider guid, ILogger<FaceDetectionService> logger)
        {
            this.detectionRepository = detectionRepository;
            this.filesService = filesService;
            this.mapper = mapper;
            this.guid = guid;
            this.logger = logger;
        }

        public async Task<int> CreateRequest(NewRequest request)
        {

            try
            {
                var detectionGuid = guid.NewGuidAsString;
                await filesService.Upload(request.Files, $"/{detectionGuid}");

                var newDetection = new DataLayer.Entities.FaceDetection
                {
                    Name = request.Name,
                    StatusId = 1,
                    Guid = detectionGuid,
                    Files = request.Files.Select(x => new File
                    {
                        Name = x.FileName,
                    }).ToList()
                };
                detectionRepository.Add(newDetection);
                detectionRepository.Save();

                return newDetection.Id;
            }
            catch (Exception e)
            {
                logger.LogError("Exception when creating face deteciton request ",e);
                throw;
            }
        }

        public async Task<IEnumerable<FaceDetectionRequest>> GetAllFaceDetectionsAsync()
        {
            var faceDetections = detectionRepository.GetAllFaces();
            try
            {
                foreach (var faceDetection in faceDetections)
                    if (faceDetection.Files.Any())
                    {
                        if (faceDetection.Files.Any(x => x.Thumbnail != null)) continue;

                        var firstImage = faceDetection.Files.FirstOrDefault();
                        if (firstImage != null) await filesService.GetThumbnail(firstImage);
                    }
            }
            catch (Exception ex)
            {
            }

            var requests = mapper.Map<IEnumerable<FaceDetectionRequest>>(faceDetections);
            return requests;
        }

        public async Task<FaceDetectionRequest> GetRequestData(int id)
        {
            var detectionJob = detectionRepository.GetRequestById(id);
            var links = await filesService.GetLinksToFilesInFolder($"/faceDetection/{id}");
            var request = mapper.Map<FaceDetectionRequest>(detectionJob);
            request.FileLinks = links;
            return request;
        }
    }
}