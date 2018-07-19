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
        private readonly IDetectionResultImageRepository detectionResultImageRepo;
        private readonly IDetectionResultRepository detectionResultRepo;
        private readonly IFilesDomainService filesService;
        private readonly ILogger<DetectionResultService> logger;
        private readonly IMapper mapper;

        public DetectionResultService(IFilesDomainService filesService, ILogger<DetectionResultService> logger,
            IMapper mapper,
            IDetectionResultRepository detectionResultRepo, IDetectionResultImageRepository detectionResultImageRepo)
        {
            this.filesService = filesService;
            this.logger = logger;
            this.mapper = mapper;
            this.detectionResultRepo = detectionResultRepo;
            this.detectionResultImageRepo = detectionResultImageRepo;
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
                        $"{ImageTypes.DetectionResultImage}/{requestId}");
                    var fileLinks = links.ToList();
                    foreach (var file in filesWithoutUrl)
                    {
                        file.Url = fileLinks.FirstOrDefault(x => x.FileName == file.Name)?.Url;
                        detectionResultImageRepo.Update(file);
                    }

                    detectionResultImageRepo.Save();
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