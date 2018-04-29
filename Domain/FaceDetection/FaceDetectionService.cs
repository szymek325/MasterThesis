using System.Collections.Generic;
using AutoMapper;
using DataLayer.Repositories.Interface;
using Domain.FaceDetection.DTO;

namespace Domain.FaceDetection
{
    public class FaceDetectionService : IFaceDetectionService
    {
        private readonly IFaceDetectionRepository detectionRepository;
        private readonly IMapper mapper;

        public FaceDetectionService(IFaceDetectionRepository detectionRepository, IMapper mapper)
        {
            this.detectionRepository = detectionRepository;
            this.mapper = mapper;
        }

        public IEnumerable<FaceDetectionRequest> GetAllFaceDetections()
        {
            var faceDetections = detectionRepository.GetAllFaces();
            var requests = mapper.Map<IEnumerable<FaceDetectionRequest>>(faceDetections);
            return requests;
        }
    }
}