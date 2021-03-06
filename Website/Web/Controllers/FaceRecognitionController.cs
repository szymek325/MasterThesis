﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.FaceRecognition;
using Domain.FaceRecognition.DTO;
using Domain.Files.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class FaceRecognitionController : Controller
    {
        private readonly IFaceRecognitionService faceRecognitionService;
        private readonly IMapper mapper;
        private readonly ILogger<FaceRecognitionController> logger;

        public FaceRecognitionController(IFaceRecognitionService faceRecognitionService, IMapper mapper,
            ILogger<FaceRecognitionController> logger)
        {
            this.faceRecognitionService = faceRecognitionService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<RecognitionRequest>> GetAll()
        {
            try
            {
                var requests= await faceRecognitionService.GetAllFaceRecognitions();
                return requests;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception when loading all Recognition requests");
                throw;
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(IFormCollection collections)
        {
            var files = mapper.Map<IEnumerable<FileToUpload>>(collections.Files);
            collections.TryGetValue("name", out var requestName);
            collections.TryGetValue("neuralNetworkId", out var neuralNetworkId);

            var response = await faceRecognitionService.CreateRequest(new NewRequest
            {
                Name = requestName,
                NeuralNetworkId = int.Parse(neuralNetworkId),
                Files = files
            });
            return Ok(new {faceRecognitionId = response});
        }

        [HttpGet("[action]")]
        public async Task<RecognitionRequest> GetRequest(int id)
        {
            try
            {
                var request = await faceRecognitionService.GetRequestData(id);
                return request;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Exception when loading face recognition {id}");
                throw;
            }
            
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<RecognitionResultOutput>> GetResultsForRequest(int id)
        {
            try
            {
                var request = await faceRecognitionService.GetResultsForRequest(id);
                return request;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Exception when loading face recognition results for {id}");
                throw;
            }

        }
    }
}