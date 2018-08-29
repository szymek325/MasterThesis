using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Repositories.Base;

namespace DataLayer.Repositories.Interface
{
    public interface IMovementRepository : IGenericRepository<Movement>
    {
        IEnumerable<Movement> GetAllWithImages();
    }
}