using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Repositories.Interface;
using Microsoft.Extensions.Logging;

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

        public Task Create()
        {
            try
            {
                var people = personRepo.GetAllPeople().ToList();
                var neural = new DataLayer.Entities.NeuralNetwork
                {
                    Name = "test2",
                };
                foreach (var person in people)
                {
                    neural.People.Add(person);
                }
                nnRepo.Add(neural);
                nnRepo.Save();
            }
            catch(Exception exception)
            {
                logger.LogError("exception on",exception);
            }

            return Task.CompletedTask;
        }

        public Task GetAll()
        {
            var neuralNetworks = nnRepo.GetAllNeuralNetworks();
            return Task.CompletedTask;
        }
    }
}