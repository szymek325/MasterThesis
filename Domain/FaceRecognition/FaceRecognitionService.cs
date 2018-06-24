using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Repositories.Interface;
using Domain.Configuration;
using Domain.FaceRecognition.DTO;
using Domain.Files;
using Microsoft.Extensions.Logging;

namespace Domain.FaceRecognition
{
    public class FaceRecognitionService : IFaceRecognitionService
    {
        private readonly IFilesDomainService filesService;
        private readonly IGuidProvider guid;
        private readonly ILogger<FaceRecognitionService> logger;
        private readonly IMapper mapper;
        private readonly IRecognitionImageRepository recognitionImagesRepository;
        private readonly IRecognitionRepository recoRepo;

        public FaceRecognitionService(IRecognitionRepository recoRepo, IFilesDomainService filesService, IMapper mapper,
            IGuidProvider guid,
            ILogger<FaceRecognitionService> logger, IRecognitionImageRepository recognitionImagesRepository)
        {
            this.recoRepo = recoRepo;
            this.filesService = filesService;
            this.mapper = mapper;
            this.guid = guid;
            this.logger = logger;
            this.recognitionImagesRepository = recognitionImagesRepository;
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
                var newRecognition = new Recognition
                {
                    Name = request.Name,
                    StatusId = 1,
                    NeuralNetworkId = request.NeuralNetworkId,
                    Images = request.Files.Select(x => new RecognitionImage
                    {
                        Name = x.FileName
                    }).ToList()
                };
                recoRepo.Add(newRecognition);
                recoRepo.Save();

                await filesService.Upload(request.Files, $"{nameof(RecognitionImage)}/{newRecognition.Id}");

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
                var links = await filesService.GetLinksToFilesInFolder(
                    $"{nameof(RecognitionImage)}/{recognitionJob.Id}");

                foreach (var file in filesWithoutUrl)
                {
                    file.Url = links.FirstOrDefault(x => x.FileName == file.Name)?.Url;
                    recognitionImagesRepository.Update(file);
                }

                recognitionImagesRepository.Save();
            }

            var request = mapper.Map<FaceRecoRequest>(recognitionJob);
            return request;
        }
    }
}