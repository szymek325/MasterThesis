using System.Threading.Tasks;
using AutoMapper;
using Domain.NeuralNetwork;
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
            await neuralNetworkService.Create();
            return Ok(new {task_Id = 1});
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            await neuralNetworkService.GetAll();
            return Ok(new {task_Id = 1});
        }
    }
}