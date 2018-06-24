﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataLayer.Entities;
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
                    var person = personRepo.GetPersonById(int.Parse(personId));
                    neuralNetwork.People.Add(person);
                }

                nnRepo.Add(neuralNetwork);
                nnRepo.Save();
                return neuralNetwork.Id;
            }
            catch (Exception exception)
            {
                logger.LogError("exception on", exception);
                throw;
            }
        }

        public NeuralNetworkOutput GetById(int id)
        {
            var neuralNetwork = nnRepo.GetById(id);
            var output = mapper.Map<NeuralNetworkOutput>(neuralNetwork);
            return output;
        }

        public IEnumerable<NeuralNetworkOutput> GetAll()
        {
            var neuralNetworks = nnRepo.GetAllNeuralNetworks().ToList();
            var output = mapper.Map<IEnumerable<NeuralNetworkOutput>>(neuralNetworks);
            return output;
        }
    }
}