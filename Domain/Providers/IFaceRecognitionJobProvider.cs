using System.Collections.Generic;
using DataLayer.Entities;

namespace Domain.Providers
{
    public interface IFaceRecognitionJobProvider
    {
        IEnumerable<FaceRecognitionJob> GetAll();
    }
}