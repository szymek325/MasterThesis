using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.People;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using PeopleUploader.Services.Interfaces;

namespace PeopleUploader.Services.Implementation
{
    public class PeopleCreator : IPeopleCreator
    {
        private readonly ILogger<PeopleCreator> logger;
        private readonly IPeopleService peopleService;
        private readonly IFilesProvider filesProvider;
        
        public PeopleCreator(ILogger<PeopleCreator> logger, IPeopleService peopleService, IFilesProvider filesProvider)
        {
            this.logger = logger;
            this.peopleService = peopleService;
            this.filesProvider = filesProvider;
        }

        public void AddPeopleToSystem()
        {
            var files = filesProvider.GetFiles();
            var distinctPerson = files.Select(x => x.PersonName).Distinct().ToList();
            Console.WriteLine($"Found {distinctPerson} distinct people");
            foreach (var person in distinctPerson)
            {
                //var 
            }
            //todo
            // 1. get people files
            // 2. convert files to people objects
            // 3. send to peopleservice
        }
    }
}
