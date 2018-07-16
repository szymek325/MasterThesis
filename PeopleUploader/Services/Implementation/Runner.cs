using Microsoft.Extensions.Logging;
using System;
using DataLayer.Configuration;
using Domain.People;
using Microsoft.Extensions.Options;
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

        public void Start()
        {
            try
            {
                peopleCreator.AddPeopleToSystem();
            }
            catch (Exception ex)

            {
                logger.LogError("error",ex);
            }
        }
    }
}