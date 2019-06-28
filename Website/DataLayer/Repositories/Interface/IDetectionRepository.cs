using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Repositories.Base;

namespace DataLayer.Repositories.Interface
{
    public interface IDetectionRepository : IGenericRepository<Detection>
    {
        IEnumerable<Detection> GetAllFaces();
        Detection GetRequestById(int id);
    }
}