using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Repositories.Interface;
using Domain.FaceRecognition.DTO;
using Domain.Files;
using Domain.Files.Helpers;
using Microsoft.Extensions.Logging;

namespace Domain.FaceRecognition
{
    public class FaceRecognitionService : IFaceRecognitionService
    {
        private readonly IFilesDomainService filesService;
        private readonly ILogger<FaceRecognitionService> logger;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;
        private readonly IRecognitionResultRepository recognitionResultRepository;
        private readonly IRecognitionRepository recoRepo;

        public FaceRecognitionService(IFilesDomainService filesService, ILogger<FaceRecognitionService> logger,
            IMapper mapper, IImageRepository imageRepository, IRecognitionResultRepository recognitionResultRepository,
            IRecognitionRepository recoRepo)
        {
            this.filesService = filesService;
            this.logger = logger;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
            this.recognitionResultRepository = recognitionResultRepository;
            this.recoRepo = recoRepo;
        }

        public async Task<IEnumerable<RecognitionRequest>> GetAllFaceRecognitions()
        {
            var faceRecognitions = recoRepo.GetAllFacesWithFullNeuralNetwork().ToList();
            try
            {
                foreach (var recognition in faceRecognitions)
                    if (string.IsNullOrWhiteSpace(recognition.Image.Thumbnail))
                        await filesService.GetThumbnail(recognition.Image);
            }
            catch (Exception ex)
            {
                logger.LogError("Exception when trying to obtain thumbnails of FR Requests", ex);
            }

            var requests = mapper.Map<IEnumerable<RecognitionRequest>>(faceRecognitions);
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
                    Image = request.Files.Select(x => new ImageAttachment
                    {
                        Name = x.FileName,
                        ImageAttachmentTypeId = ImageTypes.RecognitionImage
                    }).FirstOrDefault()
                };
                recoRepo.Add(newRecognition);
                recoRepo.Save();

                await filesService.Upload(request.Files, $"{nameof(ImageTypes.RecognitionImage)}/{newRecognition.Id}");

                return newRecognition.Id;
            }
            catch (Exception ex)
            {
                logger.LogError("Exception when creating face recognition request ", ex);
                throw;
            }
        }

        public async Task<RecognitionRequest> GetRequestData(int id)
        {
            var recognitionJob = recoRepo.GetRequestById(id);

            if (string.IsNullOrWhiteSpace(recognitionJob.Image.Url))
                await filesService.GetLinkToFile(recognitionJob.Image);

            var request = mapper.Map<RecognitionRequest>(recognitionJob);
            return request;
        }

        public async Task<IEnumerable<RecognitionResultOutput>> GetResultsForRequest(int id)
        {
            var results = recognitionResultRepository.GetAllConnectedToRequestById(id);
            var output = mapper.Map<IEnumerable<RecognitionResultOutput>>(results);
            return await Task.FromResult(output);
        }
    }
}