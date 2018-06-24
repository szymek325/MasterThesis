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
        private readonly IRecognitionRepository recoRepo;
        private readonly IFilesDomainService filesService;
        private readonly IMapper mapper;
        private readonly IGuidProvider guid;
        private readonly ILogger<FaceRecognitionService> logger;
        private readonly IDetectionImageRepository detectionImagesRepository;

        public FaceRecognitionService(IRecognitionRepository recoRepo, IFilesDomainService filesService, IMapper mapper,
            IGuidProvider guid, ILogger<FaceRecognitionService> logger, IDetectionImageRepository detectionImagesRepository)
        {
            this.recoRepo = recoRepo;
            this.filesService = filesService;
            this.mapper = mapper;
            this.guid = guid;
            this.logger = logger;
            this.detectionImagesRepository = detectionImagesRepository;
        }

        public async Task<IEnumerable<FaceRecoRequest>> GetAllFaceRecognitions()
        {
            var faceRecognitions = recoRepo.GetAllFaces().ToList();
            try
            {
                foreach (var faceDetection in faceRecognitions)
                    if (faceDetection.Images.Any() && string.IsNullOrWhiteSpace(faceDetection.Images.First().Thumbnail))
                        await filesService.GetThumbnail(faceDetection.Images.First());
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
                await filesService.Upload(request.Files, $"{faceRecognitionGuid}");

                var newRecognition = new DataLayer.Entities.Recognition()
                {
                    Name = request.Name,
                    StatusId = 1,
                    NeuralNetworkId = request.NeuralNetworkId,
                    Guid = faceRecognitionGuid,
                    Images = request.Files.Select(x => new File
                    {
                        Name = x.FileName,
                        ParentGuid = faceRecognitionGuid
                    }).ToList()
                };
                recoRepo.Add(newRecognition);
                recoRepo.Save();

                return newRecognition.Id;
            }
            catch (Exception ex)
            {
                logger.LogError("Exception when creating face recognition request ", ex);
                throw;
            }
        }

        public async Task<FaceRecoRequest> GetRequestDataAsync(int id)
        {
            var recognitionJob = recoRepo.GetRequestById(id);
            var filesWithoutUrl = recognitionJob.Images.Where(x => x.Url == null).ToList();
            if (filesWithoutUrl.Any())
            {
                var links = await filesService.GetLinksToFilesInFolder($"/{recognitionJob.Guid}");
                foreach (var file in filesWithoutUrl)
                {
                    file.Url = links.FirstOrDefault(x => x.FileName == file.Name)?.Url;
                    detectionImagesRepository.Update(file);
                }

                detectionImagesRepository.Save();
            }

            var request = mapper.Map<FaceRecoRequest>(recognitionJob);
            return request;
        }
    }
}