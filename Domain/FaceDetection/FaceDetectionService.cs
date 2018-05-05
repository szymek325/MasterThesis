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
        private readonly IGuidProvider guid;
        private readonly ILogger<FaceDetectionService> logger;
        private readonly IMapper mapper;
        private readonly IFileRepository filesRepository;

        public FaceDetectionService(IFaceDetectionRepository detectionRepository, IFilesDomainService filesService, IGuidProvider guid,
            ILogger<FaceDetectionService> logger, IMapper mapper, IFileRepository filesRepository)
        {
            this.detectionRepository = detectionRepository;
            this.filesService = filesService;
            this.guid = guid;
            this.logger = logger;
            this.mapper = mapper;
            this.filesRepository = filesRepository;
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
                        Name = x.FileName
                    }).ToList()
                };
                detectionRepository.Add(newDetection);
                detectionRepository.Save();

                return newDetection.Id;
            }
            catch (Exception ex)
            {
                logger.LogError("Exception when creating face deteciton request ", ex);
                throw;
            }
        }

        public async Task<IEnumerable<FaceDetectionRequest>> GetAllFaceDetectionsAsync()
        {
            var faceDetections = detectionRepository.GetAllFaces().ToList();
            try
            {
                foreach (var faceDetection in faceDetections)
                    if (faceDetection.Files.Any() && string.IsNullOrWhiteSpace(faceDetection.Files.First().Thumbnail))
                        await filesService.GetThumbnail(faceDetection.Files.First());
            }
            catch (Exception ex)
            {
                logger.LogError("Exception when trying to obtain thumbnails of FD Requests", ex);
            }

            var requests = mapper.Map<IEnumerable<FaceDetectionRequest>>(faceDetections);
            return requests;
        }

        public async Task<FaceDetectionRequest> GetRequestData(int id)
        {
            var detectionJob = detectionRepository.GetRequestById(id);
            var filesWithoutUrl = detectionJob.Files.Where(x => x.Url == null).ToList();
            if (filesWithoutUrl.Any())
            {
                var links = await filesService.GetLinksToFilesInFolder($"/{detectionJob.Guid}");
                foreach (var file in filesWithoutUrl)
                {
                    file.Url = links.FirstOrDefault(x => x.FileName == file.Name)?.Url;
                    filesRepository.Update(file);
                }

                filesRepository.Save();
            }
            var request = mapper.Map<FaceDetectionRequest>(detectionJob);
            return request;
        }
    }
}