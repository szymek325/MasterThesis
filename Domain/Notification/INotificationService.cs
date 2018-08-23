using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Notification.DTO;

namespace Domain.Notification
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationOutput>> GetAllNotifications();
    }
}