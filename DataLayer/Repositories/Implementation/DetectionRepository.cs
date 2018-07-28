using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementation
{
    public class DetectionRepository : GenericRepository<Detection>, IDetectionRepository
    {
        public DetectionRepository(MasterContext context) : base(context)
        {
        }

        public IEnumerable<Detection> GetAllFaces()
        {
            return GetAll().Include(x => x.Status).Include(x=>x.Image).AsEnumerable();
        }

        public Detection GetRequestById(int id)
        {
            return GetAll()
                .Include(x => x.Status)
                .Include(x => x.Image)
                .Include(x=>x.Results)
                    .ThenInclude(x=>x.DetectionType)
                .Include(x => x.Results)
                    .ThenInclude(x => x.Image)
                .Include(x => x.Results)
                    .ThenInclude(x => x.FaceRectangles)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}