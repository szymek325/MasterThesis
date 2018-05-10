using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Repositories.Interface;
using Domain.Configuration;
using Domain.FaceRecognition.DTO;
using Microsoft.Extensions.Logging;

namespace Domain.FaceRecognition
{
    public class FaceRecognitionService : IFaceRecognitionService
    {
        private readonly IFaceRecognitionRepository faceRecoRepo;
        private readonly IMapper mapper;
        private readonly IGuidProvider guid;
        private readonly ILogger<FaceRecognitionService> logger;

        public FaceRecognitionService(IFaceRecognitionRepository faceRecoRepo, IMapper mapper, IGuidProvider guid,
            ILogger<FaceRecognitionService> logger)
        {
            this.faceRecoRepo = faceRecoRepo;
            this.mapper = mapper;
            this.guid = guid;
            this.logger = logger;
        }

        public Task<IEnumerable<FaceRecoRequest>> GetAllFaceDetectionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateRequest(NewRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<FaceRecoRequest> GetRequestData(int id)
        {
            throw new NotImplementedException();
        }
    }
}