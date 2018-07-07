using System.Collections.Generic;
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

        public IEnumerable<Recognition> GetAllFaces()
        {
            return GetAll().Include(x => x.Status).Include(x => x.Images).Include(x=>x.NeuralNetwork).AsEnumerable();
        }

        public Recognition GetRequestById(int id)
        {
            return GetAll().Include(x => x.Status).Include(x => x.Images).Include(x => x.NeuralNetwork).FirstOrDefault(x => x.Id == id);
        }
    }
}