using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Repositories.Base;

namespace DataLayer.Repositories.Interface
{
    public interface IRecognitionRepository : IGenericRepository<Recognition>
    {
        IEnumerable<Recognition> GetAllFaces();
        Recognition GetRequestById(int id);
    }
}