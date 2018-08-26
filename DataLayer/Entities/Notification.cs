using DataLayer.Entities.Common;

namespace DataLayer.Entities
{
    public class Notification : EntityBase
    {
        public string Message { get; set; }
        public ImageAttachment Image { get; set; }
        public int NotificationTypeId { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}