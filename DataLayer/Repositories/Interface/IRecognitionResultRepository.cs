using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Repositories.Base;

namespace DataLayer.Repositories.Interface
{
    public interface IRecognitionResultRepository : IGenericRepository<RecognitionResult>
    {
        IEnumerable<RecognitionResult> GetAllConnectedToRequestById(int id);
    }
}