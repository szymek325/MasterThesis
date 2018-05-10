using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.FaceRecognition.DTO;

namespace Domain.FaceRecognition
{
    public class FaceRecognitionService : IFaceRecognitionService
    {
        public Task<IEnumerable<FaceRecoRequest>> GetAllFaceDetectionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateRequest(NewRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<FaceRecoRequest> GetRequestData(int id)
        {
            throw new NotImplementedException();
        }
    }
}