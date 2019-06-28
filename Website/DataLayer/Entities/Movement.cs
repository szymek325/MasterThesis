using DataLayer.Entities.Common;

namespace DataLayer.Entities
{
    public  class Movement : EntityBase
    {
        public string Message { get; set; }
        public ImageAttachment Image { get; set; }
    }
}