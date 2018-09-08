using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.NeuralNetwork;
using Domain.NeuralNetwork.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class NeuralNetworkController : Controller
    {
        private readonly ILogger<NeuralNetworkController> logger;
        private readonly IMapper mapper;
        private readonly INeuralNetworkService neuralNetworkService;

        public NeuralNetworkController(INeuralNetworkService neuralNetworkService, IMapper mapper,
            ILogger<NeuralNetworkController> logger)
        {
            this.neuralNetworkService = neuralNetworkService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(IFormCollection collections)
        {
            try
            {
                collections.TryGetValue("name", out var neuralNetworkName);
                collections.TryGetValue("photosPerPerson", out var photosMax);
                collections.TryGetValue("people", out var peopleIds);
                var photosPerPerson = int.Parse(photosMax);
                var nnId= await neuralNetworkService.Create(neuralNetworkName, peopleIds, photosPerPerson);
                return Ok(new {neuralNetworkId = nnId });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception when creating neural network request");
                throw;
            }
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<AllNeuralNetworksOutput>> GetAll()
        {
            try
            {
                var response = await neuralNetworkService.GetAll();
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception when loading all neural networks");
                throw;
            }
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<NeuralNetworkBaseInfoOutput>> GetAllCompleted()
        {
            try
            {
                var response = await neuralNetworkService.GetAllCompleted();
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception when loading all completed neural networks");
                throw;
            }
        }


        [HttpGet("[action]")]
        public async Task<NeuralNetworkRequest> Get(int id)
        {
            try
            {
                var response = await neuralNetworkService.GetById(id);
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Exception when loading data for nn request :{id}");
                throw;
            }
        }
    }
}