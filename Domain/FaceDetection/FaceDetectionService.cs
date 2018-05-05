using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Repositories.Interface;
using Domain.FaceDetection.DTO;
using Domain.Files;
using Domain.Files.DTO;
using DropboxIntegration.Files;

namespace Domain.FaceDetection
{
    public class FaceDetectionService : IFaceDetectionService
    {
        private readonly IFaceDetectionRepository detectionRepository;
        private readonly IFilesDomainService filesService;
        private readonly IFilesClient filesClient;
        private readonly IMapper mapper;

        public FaceDetectionService(IFaceDetectionRepository detectionRepository, IFilesDomainService filesService, IFilesClient filesClient, IMapper mapper)
        {
            this.detectionRepository = detectionRepository;
            this.filesService = filesService;
            this.filesClient = filesClient;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<FaceDetectionRequest>> GetAllFaceDetectionsAsync()
        {
            var faceDetections = detectionRepository.GetAllFaces();
            try
            {
                foreach (var faceDetection in faceDetections)
                {
                    if (faceDetection.Files.Any())
                    {
                        if (faceDetection.Files.Any(x => x.Thumbnail != null)) continue;

                        var firstImage = faceDetection.Files.FirstOrDefault();
                        if (firstImage != null)
                        {
                            await filesService.GetThumbnail(firstImage);
                        }
                    }
                }
            }
            catch(Exception ex)
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

        public async Task<int> CreateRequest(NewRequest request)
        {
            try
            {
                var guid = Guid.NewGuid();
                await filesService.Upload(request.Files, $"/faceDetection/{guid}");

                var newDetection = new DataLayer.Entities.FaceDetection
                {
                    Name = request.Name,
                    StatusId = 1,
                    Guid = guid.ToString()
                };
                detectionRepository.Add(newDetection);
                detectionRepository.Save();

                return newDetection.Id;
            }
            catch(Exception exception)
            {

            }

            return 2;
        }
    }
}