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
        private readonly IDetectionImageRepository detectionImagesRepository;
        private readonly IDetectionRepository detectionRepository;
        private readonly IFilesDomainService filesService;
        private readonly IGuidProvider guid;
        private readonly ILogger<FaceDetectionService> logger;
        private readonly IMapper mapper;

        public FaceDetectionService(IDetectionRepository detectionRepository, IFilesDomainService filesService,
            IGuidProvider guid,
            ILogger<FaceDetectionService> logger, IMapper mapper, IDetectionImageRepository detectionImagesRepository)
        {
            this.detectionRepository = detectionRepository;
            this.filesService = filesService;
            this.guid = guid;
            this.logger = logger;
            this.mapper = mapper;
            this.detectionImagesRepository = detectionImagesRepository;
        }

        public async Task<int> CreateRequest(NewRequest request)
        {
            try
            {
                var newDetection = new Detection
                {
                    Name = request.Name,
                    StatusId = 1,
                    Images = request.Files.Select(x => new DetectionImage
                    {
                        Name = x.FileName
                    }).ToList()
                };
                detectionRepository.Add(newDetection);
                detectionRepository.Save();

                await filesService.Upload(request.Files, $"{ImageTypes.DetectionImage}/{newDetection.Id}");

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
                    if (faceDetection.Images.Any() && string.IsNullOrWhiteSpace(faceDetection.Images.First().Thumbnail))
                        await filesService.GetThumbnail(faceDetection.Images.First());
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
            var filesWithoutUrl = detectionJob.Images.Where(x => x.Url == null).ToList();
            if (filesWithoutUrl.Any())
            {
                var links = await filesService.GetLinksToFilesInFolder($"{ImageTypes.DetectionImage}/{detectionJob.Id}");

                foreach (var file in filesWithoutUrl)
                {
                    file.Url = links.FirstOrDefault(x => x.FileName == file.Name)?.Url;
                    detectionImagesRepository.Update(file);
                }

                detectionImagesRepository.Save();
            }

            var request = mapper.Map<FaceDetectionRequest>(detectionJob);
            return request;
        }
    }
}