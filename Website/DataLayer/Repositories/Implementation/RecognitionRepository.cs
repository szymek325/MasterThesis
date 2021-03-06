﻿using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementation
{
    public class RecognitionRepository : GenericRepository<Recognition>, IRecognitionRepository
    {
        public RecognitionRepository(MasterContext context) : base(context)
        {
        }

        public IEnumerable<Recognition> GetAllFacesWithFullNeuralNetwork()
        {
            return GetAll().Include(x => x.Status)
                .Include(x => x.Image)
                .Include(x => x.NeuralNetwork)
                    .ThenInclude(x => x.Files)
                .OrderByDescending(x => x.CreationTime);
        }

        public Recognition GetRequestById(int id)
        {
            return GetAll()
                .Include(x => x.Status)
                .Include(x => x.Image)
                .Include(x=>x.RecognitionResults)
                    .ThenInclude(x => x.NeuralNetworkFile)
                        .ThenInclude(x => x.NeuralNetworkType)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}