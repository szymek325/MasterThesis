using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;

namespace DataLayer.Repositories.Implementation
{
    public class ImageRepository : GenericRepository<ImageAttachment>, IImageRepository
    {
        public ImageRepository(MasterContext context) : base(context)
        {
        }
    }
}