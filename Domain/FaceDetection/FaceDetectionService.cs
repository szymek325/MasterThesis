using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Repositories.Interface;
using Domain.FaceDetection.DTO;
using Domain.Files;

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

        public int CreateRequest(NewRequest request)
        {
            var newDetection = new DataLayer.Entities.FaceDetection
            {
                Name = request.Name,
                StatusId = 1
            };
            detectionRepository.Add(newDetection);
            detectionRepository.Save();

            filesService.Upload(request.Files, $"/faceDetection/{newDetection.Id}");

            return newDetection.Id ;
        }
    }
}