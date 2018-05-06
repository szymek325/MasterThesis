using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.FaceDetection.DTO;
using Domain.Files.DTO;
using Domain.NeuralNetwork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    public class NeuralNetworkController : Controller
    {
        private readonly INeuralNetworkService neuralNetworkService;
        private readonly IMapper mapper;
        private readonly ILogger<NeuralNetworkController> logger;

        public NeuralNetworkController(INeuralNetworkService neuralNetworkService, IMapper mapper, ILogger<NeuralNetworkController> logger)
        {
            this.neuralNetworkService = neuralNetworkService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(IFormCollection collections)
        {
            neuralNetworkService.Create();
            return Ok(new { task_Id = 1 });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetAll()
        {
            neuralNetworkService.GetAll();
            return Ok(new { task_Id = 1 });
        }
    }
}