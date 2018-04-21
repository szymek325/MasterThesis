using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.Repositories.Interface;

namespace Domain.Providers
{
    public class FaceRecognitionJobProvider : IFaceRecognitionJobProvider
    {
        private readonly IFaceRecognitionJobRepository faceRepo;

        public FaceRecognitionJobProvider(IFaceRecognitionJobRepository faceRepo)
        {
            this.faceRepo = faceRepo;
        }

        public IEnumerable<FaceRecognitionJob> GetAll()
        {
            var jobs = faceRepo.Get().ToList();
            return jobs;
        }
    }
}