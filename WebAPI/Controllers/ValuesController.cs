using System.Collections.Generic;
using DataLayer.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IFaceRecognitionJobRepository jobsRepo;
        private readonly ILogger<ValuesController> logger;

        public ValuesController(ILogger<ValuesController> logger, IFaceRecognitionJobRepository jobsRepo)
        {
            this.logger = logger;
            this.jobsRepo = jobsRepo;
        }


        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            jobsRepo.Get(x => x.Id == 2);
            return new[] {"value1", "value2"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            logger.LogInformation("super ID logged");
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}