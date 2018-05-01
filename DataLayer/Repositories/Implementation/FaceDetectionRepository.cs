using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementation
{
    public class FaceDetectionRepository : GenericRepository<FaceDetection>, IFaceDetectionRepository
    {
        public FaceDetectionRepository(MasterContext context) : base(context)
        {
        }

        public IEnumerable<FaceDetection> GetAllFaces()
        {
            return GetAll().Include(x => x.Status).AsEnumerable();
        }

        public FaceDetection GetRequestById(int id)
        {
            return GetAll().Include(x => x.Status).FirstOrDefault(x => x.Id == id);
        }
    }
}