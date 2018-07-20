using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Repositories.Interface;
using Domain.FaceDetection.DTO;
using Domain.Files;
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
                var results = detectionResultRepo.GetResultsConnectedToRequest(requestId).ToList();

                foreach (var result in results)
                {
                    if (string.IsNullOrWhiteSpace(result.Image.Url))
                        await filesService.GetLinkToFile(result.Image);
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