using System.Collections.Generic;
using Domain.Notification.DTO;

namespace WebRazor.Models.Notification
{
    public class NotificationsViewModel
    {
        public NotificationsViewModel(IEnumerable<NotificationOutput> notifications)
        {
            Notifications = notifications;
        }

        public IEnumerable<NotificationOutput> Notifications { get; }
    }
}