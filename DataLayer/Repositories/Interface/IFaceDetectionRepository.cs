using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Repositories.Base;

namespace DataLayer.Repositories.Interface
{
    public interface IFaceDetectionRepository : IGenericRepository<FaceDetection>
    {
        IEnumerable<FaceDetection> GetAllFaces();
        FaceDetection GetRequestById(int id);
    }
}