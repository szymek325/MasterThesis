using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;

namespace DataLayer.Repositories.Implementation
{
    public class FaceRecognitionJobRepository : GenericRepository<FaceRecognitionJob>, IFaceRecognitionJobRepository
    {
        public FaceRecognitionJobRepository(MasterContext context) : base(context)
        {
        }

    }
}