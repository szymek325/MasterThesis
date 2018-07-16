using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PeopleUploader.Services.Interfaces;

namespace PeopleUploader.Services.Implementation
{
    public class Runner : IRunner
    {
        private readonly ILogger<Runner> logger;
        private readonly IPeopleCreator peopleCreator;

        public Runner(ILogger<Runner> logger, IPeopleCreator peopleCreator)
        {
            this.logger = logger;
            this.peopleCreator = peopleCreator;
        }

        public async Task Start()
        {
            try
            {
                await peopleCreator.AddPeopleToSystem();
            }
            catch (Exception ex)

            {
                logger.LogError("error", ex);
            }
        }
    }
}