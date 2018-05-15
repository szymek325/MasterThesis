using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementation
{
    public class FaceRecognitionRepository : GenericRepository<FaceRecognition>, IFaceRecognitionRepository
    {
        public FaceRecognitionRepository(MasterContext context) : base(context)
        {
        }

        public IEnumerable<FaceRecognition> GetAllFaces()
        {
            return GetAll().Include(x => x.Status).Include(x => x.Files).Include(x=>x.NeuralNetwork).AsEnumerable();
        }

        public FaceRecognition GetRequestById(int id)
        {
            return GetAll().Include(x => x.Status).Include(x => x.Files).Include(x => x.NeuralNetwork).FirstOrDefault(x => x.Id == id);
        }
    }
}