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

namespace Domain.FaceDetection
{
    public class FaceDetectionService : IFaceDetectionService
    {
        private readonly IFaceDetectionRepository detectionRepository;
        private readonly IFilesDomainService filesService;
        private readonly IMapper mapper;

        public FaceDetectionService(IFaceDetectionRepository detectionRepository, IFilesDomainService filesService,
            IMapper mapper)
        {
            this.detectionRepository = detectionRepository;
            this.filesService = filesService;
            this.mapper = mapper;
        }

        public IEnumerable<FaceDetectionRequest> GetAllFaceDetections()
        {
            var faceDetections = detectionRepository.GetAllFaces();
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
            var attachment = request.Files.FirstOrDefault();
            if (attachment == null)
            {
                return 0;
            }

            var newDetection = new DataLayer.Entities.FaceDetection
            {
                Name = request.Name,
                StatusId = 1,
                File = new File
                {
                    Name = attachment.FileName,
                    FileSourceId = 2
                }
            };
            detectionRepository.Add(newDetection);
            detectionRepository.Save();
            attachment.FileName = "input." + attachment.FileName.Split('.').Last();
            await filesService.Upload(request.Files, $"/faceDetection/{newDetection.Id}");

            return newDetection.Id ;
        }
    }
}