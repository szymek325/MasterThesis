using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Repositories.Base;

namespace DataLayer.Repositories.Interface
{
    public interface IFaceRecognitionRepository : IGenericRepository<FaceRecognition>
    {
        IEnumerable<FaceRecognition> GetAllFaces();
        FaceRecognition GetRequestById(int id);
    }
}