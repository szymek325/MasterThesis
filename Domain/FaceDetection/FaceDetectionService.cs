using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Repositories.Interface;
using Domain.FaceDetection.DTO;
using Domain.Files;
using Domain.Files.Helpers;
using Microsoft.Extensions.Logging;

namespace Domain.FaceDetection
{
    public class FaceDetectionService : IFaceDetectionService
    {
        private readonly IDetectionRepository detectionRepository;
        private readonly IFilesDomainService filesService;
        private readonly ILogger<FaceDetectionService> logger;
        private readonly IMapper mapper;

        public FaceDetectionService(IDetectionRepository detectionRepository, IFilesDomainService filesService,
            ILogger<FaceDetectionService> logger, IMapper mapper)
        {
            this.detectionRepository = detectionRepository;
            this.filesService = filesService;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<int> CreateRequest(NewRequest request)
        {
            try
            {
                var newDetection = new Detection
                {
                    Name = request.Name,
                    StatusId = 1,
                    Image = request.Files.Select(x => new ImageAttachment()
                    {
                        Name = x.FileName,
                        ImageAttachmentTypeId = ImageTypes.DetectionImage
                    }).FirstOrDefault()
                };
                detectionRepository.Add(newDetection);
                detectionRepository.Save();

                await filesService.Upload(request.Files, $"{nameof(ImageTypes.DetectionImage)}/{newDetection.Id}");

                return newDetection.Id;
            }
            catch (Exception ex)
            {
                logger.LogError("Exception when creating face deteciton request ", ex);
                throw new Exception("Exception when creating face deteciton request ");
            }
        }

        public async Task<IEnumerable<DetectionRequest>> GetAllFaceDetectionsAsync()
        {
            var faceDetections = detectionRepository.GetAllFaces().ToList();
            try
            {
                foreach (var faceDetection in faceDetections)
                    if (string.IsNullOrWhiteSpace(faceDetection.Image.Thumbnail))
                        await filesService.GetThumbnail(faceDetection.Image);
            }
            catch (Exception ex)
            {
                logger.LogError("Exception when trying to obtain thumbnails of FD Requests", ex);
            }

            var requests = mapper.Map<IEnumerable<DetectionRequest>>(faceDetections);
            return requests;
        }

        public async Task<DetectionRequest> GetRequestData(int id)
        {
            var detectionJob = detectionRepository.GetRequestById(id);

            if (string.IsNullOrWhiteSpace(detectionJob.Image.Url))
                await filesService.GetLinkToFile(detectionJob.Image);

            var request = mapper.Map<DetectionRequest>(detectionJob);
            return request;
        }
    }
}