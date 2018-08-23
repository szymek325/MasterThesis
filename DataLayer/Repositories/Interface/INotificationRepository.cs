using System.Collections.Generic;
using DataLayer.Entities;
using DataLayer.Repositories.Base;

namespace DataLayer.Repositories.Interface
{
    public interface INotificationRepository : IGenericRepository<Notification>
    {
        IEnumerable<Notification> GetAllMotionNotifications();

        IEnumerable<Notification> GetAllSensorNotifications();
    }
}