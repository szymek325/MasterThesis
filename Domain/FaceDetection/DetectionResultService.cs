using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Repositories.Interface;
using Domain.FaceDetection.DTO;
using Domain.Files;
using Domain.Files.Helpers;
using Microsoft.Extensions.Logging;

namespace Domain.FaceDetection
{
    public class DetectionResultService : IDetectionResultService
    {
        private readonly IImageRepository imageRepository;
        private readonly IDetectionResultRepository detectionResultRepo;
        private readonly IFilesDomainService filesService;
        private readonly ILogger<DetectionResultService> logger;
        private readonly IMapper mapper;

        public DetectionResultService(IImageRepository imageRepository, IDetectionResultRepository detectionResultRepo,
            IFilesDomainService filesService, ILogger<DetectionResultService> logger, IMapper mapper)
        {
            this.imageRepository = imageRepository;
            this.detectionResultRepo = detectionResultRepo;
            this.filesService = filesService;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<DetectionResultOutput>> GetResultsForRequest(int requestId)
        {
            try
            {
                var results = detectionResultRepo.GetResultsConnectedToRequest(requestId);
                var filesWithoutUrl = results.Where(x => string.IsNullOrWhiteSpace(x.Image.Url)).Select(x => x.Image)
                    .ToList();

                if (filesWithoutUrl.Any())
                {
                    var links = await filesService.GetLinksToFilesInFolder(
                        $"{nameof(ImageTypes.DetectionResultImage)}/{requestId}");
                    var fileLinks = links.ToList();
                    foreach (var file in filesWithoutUrl)
                    {
                        file.Url = fileLinks.FirstOrDefault(x => x.FileName == file.Name)?.Url;
                        imageRepository.Update(file);
                    }

                    imageRepository.Save();
                }

                var output = mapper.Map<IEnumerable<DetectionResultOutput>>(results);
                return output;
            }
            catch (Exception ex)
            {
                logger.LogError($"Exception when loading results for detection request :{requestId}", ex);
                throw;
            }
        }
    }
}