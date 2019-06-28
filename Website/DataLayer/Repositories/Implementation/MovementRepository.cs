using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementation
{
    public class MovementRepository : GenericRepository<Movement>, IMovementRepository
    {
        public MovementRepository(MasterContext context) : base(context)
        {
        }

        public IEnumerable<Movement> GetAllWithImages()
        {
            return GetAll().Include(x => x.Image);
        }
    }
}