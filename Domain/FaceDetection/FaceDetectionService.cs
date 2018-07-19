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
        private readonly IImageRepository imageRepository;
        private readonly IDetectionRepository detectionRepository;
        private readonly IFilesDomainService filesService;
        private readonly ILogger<FaceDetectionService> logger;
        private readonly IMapper mapper;

        public FaceDetectionService(IImageRepository imageRepository, IDetectionRepository detectionRepository,
            IFilesDomainService filesService, ILogger<FaceDetectionService> logger, IMapper mapper)
        {
            this.imageRepository = imageRepository;
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
                    Images = request.Files.Select(x => new ImageAttachment()
                    {
                        Name = x.FileName,
                        ImageAttachmentTypeId = ImagesTypesEnum.DetectionImage
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
                throw new Exception("Exception when creating face deteciton request ");
            }
        }

        public async Task<IEnumerable<DetectionRequest>> GetAllFaceDetectionsAsync()
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

            var requests = mapper.Map<IEnumerable<DetectionRequest>>(faceDetections);
            return requests;
        }

        public async Task<DetectionRequest> GetRequestData(int id)
        {
            var detectionJob = detectionRepository.GetRequestById(id);
            var filesWithoutUrl = detectionJob.Images.Where(x => x.Url == null).ToList();
            if (filesWithoutUrl.Any())
            {
                var links = await filesService.GetLinksToFilesInFolder(
                    $"{ImageTypes.DetectionImage}/{detectionJob.Id}");

                foreach (var file in filesWithoutUrl)
                {
                    file.Url = links.FirstOrDefault(x => x.FileName == file.Name)?.Url;
                    imageRepository.Update(file);
                }

                imageRepository.Save();
            }

            var request = mapper.Map<DetectionRequest>(detectionJob);
            return request;
        }
    }
}