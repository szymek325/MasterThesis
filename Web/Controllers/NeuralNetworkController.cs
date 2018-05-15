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
            collections.TryGetValue("name", out var neuralNetworkName);
            collections.TryGetValue("people", out var peopleIds);
            var nnId= neuralNetworkService.Create(neuralNetworkName, peopleIds);
            return Ok(new {neuralNetworkId = nnId });
        }

        [HttpGet("[action]")]
        public IEnumerable<NeuralNetworkOutput> GetAll()
        {
            var response = neuralNetworkService.GetAll();
            return response;
        }

        [HttpGet("[action]")]
        public NeuralNetworkOutput Get(int id)
        {
            var response = neuralNetworkService.GetById(id);
            return response;
        }
    }
}