using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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

        public FaceDetectionRequest GetRequestData(int id)
        {
            var lnkList = new List<FileLink>
            {
                new FileLink
                {
                    Url = "123"
                },
                new FileLink
                {
                    Url = "trest23"
                }
            };
            var request = new FaceDetectionRequest
            {
                Name = "test test",
                HaarFaces = 2,
                DnnFaces = 3,
                Id = 100,
                FileLinks = lnkList
            };
            return request;
        }

        public async Task<int> CreateRequest(NewRequest request)
        {
            var newDetection = new DataLayer.Entities.FaceDetection
            {
                Name = request.Name,
                StatusId = 1
            };
            detectionRepository.Add(newDetection);
            detectionRepository.Save();

            await filesService.Upload(request.Files, $"/faceDetection/{newDetection.Id}");

            return newDetection.Id ;
        }
    }
}