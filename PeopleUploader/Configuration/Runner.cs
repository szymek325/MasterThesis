using Microsoft.Extensions.Logging;
using System;
using DataLayer.Configuration;
using Domain.People;
using Microsoft.Extensions.Options;

namespace PeopleUploader.Configuration
{
    public class Runner : IRunner
    {
        private readonly ILogger<Runner> logger;
        private readonly IPeopleService peopleService;

        public Runner(ILogger<Runner> logger, IPeopleService peopleService)
        {
            this.logger = logger;
            this.peopleService = peopleService;
        }

        public void DoAction(string name)
        {
            try
            {
                logger.LogInformation(20, "Doing hard work! {Action}", name);
                var people=peopleService.GetAllPeople();
                logger.LogInformation(Newtonsoft.Json.JsonConvert.SerializeObject(people));
            }
            catch (Exception ex)

            {
                logger.LogError("error",ex);
            }
        }
    }
}