using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementation
{
    public class NeuralNetworkRepository : GenericRepository<NeuralNetwork>,
        INeuralNetworkRepository
    {
        public NeuralNetworkRepository(MasterContext context) : base(context)
        {
        }

        public IEnumerable<NeuralNetwork> GetAllNeuralNetworks()
        {
            return GetAll().Include("NeuralNetworkPeople.Person").AsEnumerable();
        }

        public NeuralNetwork GetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}