using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.Repositories.Interface;
using Microsoft.Extensions.Logging;


namespace Domain.Providers
{
    public class FaceRecognitionJobProvider : IFaceRecognitionJobProvider
    {
        private readonly IFaceRecognitionJobRepository faceRepo;
        private readonly ILogger<FaceRecognitionJobProvider> logger;


        public FaceRecognitionJobProvider(IFaceRecognitionJobRepository faceRepo, ILogger<FaceRecognitionJobProvider> logger)
        {
            this.faceRepo = faceRepo;
            this.logger = logger;
        }

        public IEnumerable<FaceRecognitionJob> GetAll()
        {
            var jobs = faceRepo.Get().ToList();
            logger.LogDebug($"{jobs.Count} received");
            logger.LogDebug($"{jobs.Count} works!");
            return jobs;
        }
    }
}