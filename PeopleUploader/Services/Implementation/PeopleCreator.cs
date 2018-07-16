using System;
using System.Collections.Generic;
using System.Text;
using Domain.People;
using Microsoft.Extensions.Logging;
using PeopleUploader.Services.Interfaces;

namespace PeopleUploader.Services.Implementation
{
    public class PeopleCreator : IPeopleCreator
    {
        private readonly ILogger<PeopleCreator> logger;
        private readonly IPeopleService peopleService;

        public PeopleCreator(ILogger<PeopleCreator> logger, IPeopleService peopleService)
        {
            this.logger = logger;
            this.peopleService = peopleService;
        }

        public void AddPeopleToSystem()
        {
            //todo
            // 1. get people files
            // 2. convert files to people objects
            // 3. send to peopleservice
        }
    }
}
