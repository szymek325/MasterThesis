using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Repositories.Interface;
using Domain.NeuralNetwork.DTO;
using Microsoft.Extensions.Logging;

namespace Domain.NeuralNetwork
{
    public class NeuralNetworkService : INeuralNetworkService
    {
        private readonly ILogger<NeuralNetworkService> logger;
        private readonly IMapper mapper;
        private readonly INeuralNetworkRepository nnRepo;
        private readonly IPersonRepository personRepo;

        public NeuralNetworkService(INeuralNetworkRepository nnRepo, IPersonRepository personRepo,
            ILogger<NeuralNetworkService> logger, IMapper mapper)
        {
            this.nnRepo = nnRepo;
            this.personRepo = personRepo;
            this.logger = logger;
            this.mapper = mapper;
        }

        public Task<int> Create(string neuralNetworkName, string peopleIds)
        {
            var neuralNetwork = new DataLayer.Entities.NeuralNetwork
            {
                Name = neuralNetworkName,
                StatusId = 1
            };
            foreach (var personId in peopleIds.Split(','))
            {
                var person = personRepo.GetPersonById(int.Parse(personId));
                neuralNetwork.People.Add(person);
            }

            nnRepo.Add(neuralNetwork);
            nnRepo.Save();
            return Task.FromResult(neuralNetwork.Id);
        }

        public Task<NeuralNetworkRequest> GetById(int id)
        {
            var neuralNetwork = nnRepo.GetById(id);
            var output = mapper.Map<NeuralNetworkRequest>(neuralNetwork);
            return Task.FromResult(output);
        }

        public Task<IEnumerable<AllNeuralNetworksOutput>> GetAll()
        {
            var neuralNetworks = nnRepo.GetAllNeuralNetworksWithDependencies().ToList();
            var output = mapper.Map<IEnumerable<AllNeuralNetworksOutput>>(neuralNetworks);
            return Task.FromResult(output);
        }

        public Task<IEnumerable<NeuralNetworkBaseInfoOutput>> GetAllCompleted()
        {
            var neuralNetworks = nnRepo.GetAllCompleted();
            var output = mapper.Map<IEnumerable<NeuralNetworkBaseInfoOutput>>(neuralNetworks);
            return Task.FromResult(output);
        }
    }
}