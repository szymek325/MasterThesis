using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.Helpers;
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

        public IEnumerable<NeuralNetwork> GetAllNeuralNetworksWithDependencies()
        {
            return GetAll().Include("NeuralNetworkPeople.Person").Include(x => x.Status)
                .Include(x => x.Files);
        }

        public NeuralNetwork GetById(int id)
        {
            return GetAll().Include("NeuralNetworkPeople.Person").Include(x => x.Status).Include(x => x.Files)
                .ThenInclude(y => y.NeuralNetworkType).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<NeuralNetwork> GetAllCompleted()
        {
            return GetAll().Where(x => x.StatusId == StatusTypes.Completed);
        }
    }
}