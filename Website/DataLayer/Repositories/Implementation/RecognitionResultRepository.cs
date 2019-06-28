using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementation
{
    public class RecognitionResultRepository : GenericRepository<RecognitionResult>, IRecognitionResultRepository
    {
        public RecognitionResultRepository(MasterContext context) : base(context)
        {
        }

        public IEnumerable<RecognitionResult> GetAllConnectedToRequestById(int id)
        {
            return GetAll().Include(x => x.NeuralNetworkFile).ThenInclude(x => x.NeuralNetworkType)
                .Where(x => x.RecognitionId == id);
        }
    }
}