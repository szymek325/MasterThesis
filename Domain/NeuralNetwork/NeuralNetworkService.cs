using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Repositories.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Domain.NeuralNetwork
{
    public class NeuralNetworkService : INeuralNetworkService
    {
        private readonly INeuralNetworkRepository nnRepo;
        private readonly IPersonRepository personRepo;
        private readonly ILogger<NeuralNetworkService> logger;

        public NeuralNetworkService(INeuralNetworkRepository nnRepo, IPersonRepository personRepo, ILogger<NeuralNetworkService> logger)
        {
            this.nnRepo = nnRepo;
            this.personRepo = personRepo;
            this.logger = logger;
        }

        public int Create(string neuralNetworkName, string peopleIds)
        {
            try
            {
                var neuralNetwork = new DataLayer.Entities.NeuralNetwork
                {
                    Name = neuralNetworkName,
                    StatusId = 1
                };
                foreach (var personId in peopleIds.Split(','))
                {
                    var person=personRepo.GetPersonById(int.Parse(personId));
                    neuralNetwork.People.Add(person);
                }

                nnRepo.Add(neuralNetwork);
                nnRepo.Save();
                return neuralNetwork.Id;
            }
            catch(Exception exception)
            {
                logger.LogError("exception on",exception);
                throw;
            }
        }

        public Task GetAll()
        {
            var neuralNetworks = nnRepo.GetAllNeuralNetworks();
            return Task.CompletedTask;
        }
    }
}