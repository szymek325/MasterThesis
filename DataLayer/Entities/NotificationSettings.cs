using DataLayer.Entities.Common;

namespace DataLayer.Entities
{
    public class NotificationSettings : EntityBase
    {
        public string Name { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}