using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Repositories.Interface;
using Domain.FaceDetection.DTO;
using Domain.Files;
using Microsoft.Extensions.Logging;

namespace Domain.FaceDetection
{
    public class DetectionResultService
    {
        private readonly IFilesDomainService filesService;
        private readonly ILogger<DetectionResultService> logger;
        private readonly IMapper mapper;
        private readonly IDetectionResultRepository detectionResultRepo;
        private readonly IDetectionResultImageRepository detectionResultImageRepo;

        public DetectionResultService(IFilesDomainService filesService, ILogger<DetectionResultService> logger, IMapper mapper, IDetectionResultRepository detectionResultRepo)
        {
            this.filesService = filesService;
            this.logger = logger;
            this.mapper = mapper;
            this.detectionResultRepo = detectionResultRepo;
        }

        public async Task<IEnumerable<DetectionResultOutput>> GetResultsForRequest(int requestId)
        {
            try
            {
                var results=detectionResultRepo.GetResultsConnectedToRequest(requestId);
                var filesWithoutUrl = results.Where(x => string.IsNullOrWhiteSpace(x.Image.Url)).Select(x => x.Image).ToList();
                   
                if (filesWithoutUrl.Any())
                {
                    var links = await filesService.GetLinksToFilesInFolder(
                        $"{ImageTypes.DetectionResultImage}/{requestId}");

                    foreach (var file in filesWithoutUrl)
                    {
                        file.Url = links.FirstOrDefault(x => x.FileName == file.Name)?.Url;
                        detectionImagesRepository.Update(file);
                    }

                    detectionImagesRepository.Save();
                }


                var output = mapper.Map<IEnumerable<DetectionResultOutput>>(results);
                return output;
            }
            catch (Exception ex)
            {

            }
        }

    }
}
