using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Helpers;
using DataLayer.Repositories.Interface;
using Domain.FaceDetection.DTO;
using Domain.Files;
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
                        ImageAttachmentTypeId = ImageTypes.Detection
                    }).FirstOrDefault()
                };
                detectionRepository.Add(newDetection);
                detectionRepository.Save();

                await filesService.Upload(request.Files, $"{nameof(ImageTypes.Detection)}/{newDetection.Id}");

                return newDetection.Id;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception when creating face deteciton request ");
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
                logger.LogError(ex, "Exception when trying to obtain thumbnails of FD Requests");
            }

            var requests = mapper.Map<IEnumerable<DetectionRequest>>(faceDetections);
            return requests;
        }

        public async Task<DetectionRequest> GetRequestData(int id)
        {
            var detectionJob = detectionRepository.GetRequestById(id);

            if (string.IsNullOrWhiteSpace(detectionJob.Image.Url))
                await filesService.GetLinkToFile(detectionJob.Image);

            foreach (var detectionJobResult in detectionJob.Results)
            {
                if (string.IsNullOrWhiteSpace(detectionJobResult.Image.Url))
                    await filesService.GetLinkToFile(detectionJobResult.Image);
            }

            var request = mapper.Map<DetectionRequest>(detectionJob);
            return request;
        }
    }
}