using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Repositories.Interface;
using Domain.Configuration;
using Domain.FaceDetection.DTO;
using Domain.FaceRecognition.DTO;
using Domain.Files;
using Microsoft.Extensions.Logging;
using NewRequest = Domain.FaceRecognition.DTO.NewRequest;

namespace Domain.FaceRecognition
{
    public class FaceRecognitionService : IFaceRecognitionService
    {
        private readonly IFaceRecognitionRepository faceRecoRepo;
        private readonly IFilesDomainService filesService;
        private readonly IMapper mapper;
        private readonly IGuidProvider guid;
        private readonly ILogger<FaceRecognitionService> logger;

        public FaceRecognitionService(IFaceRecognitionRepository faceRecoRepo, IFilesDomainService filesService,
            IMapper mapper, IGuidProvider guid, ILogger<FaceRecognitionService> logger)
        {
            this.faceRecoRepo = faceRecoRepo;
            this.filesService = filesService;
            this.mapper = mapper;
            this.guid = guid;
            this.logger = logger;
        }

        public async Task<IEnumerable<FaceRecoRequest>> GetAllFaceRecognitions()
        {
            var faceRecognitions = faceRecoRepo.GetAllFaces().ToList();
            try
            {
                foreach (var faceDetection in faceRecognitions)
                    if (faceDetection.Files.Any() && string.IsNullOrWhiteSpace(faceDetection.Files.First().Thumbnail))
                        await filesService.GetThumbnail(faceDetection.Files.First());
            }
            catch (Exception ex)
            {
                logger.LogError("Exception when trying to obtain thumbnails of FR Requests", ex);
            }

            var requests = mapper.Map<IEnumerable<FaceRecoRequest>>(faceRecognitions);
            return requests;
        }

        public async Task<int> CreateRequest(NewRequest request)
        {
            try
            {
                var faceRecognitionGuid = guid.NewGuidAsString;
                await filesService.Upload(request.Files, $"/{faceRecognitionGuid}");

                var newRecognition = new DataLayer.Entities.FaceRecognition()
                {
                    Name = request.Name,
                    StatusId = 1,
                    NeuralNetworkId = request.NeuralNetworkId,
                    Guid = faceRecognitionGuid,
                    Files = request.Files.Select(x => new File
                    {
                        Name = x.FileName
                    }).ToList()
                };
                faceRecoRepo.Add(newRecognition);
                faceRecoRepo.Save();

                return newRecognition.Id;
            }
            catch (Exception ex)
            {
                logger.LogError("Exception when creating face recognition request ", ex);
                throw;
            }
        }

        public Task<FaceRecoRequest> GetRequestData(int id)
        {
            throw new NotImplementedException();
        }
    }
}