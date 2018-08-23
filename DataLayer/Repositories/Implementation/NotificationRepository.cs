using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.Helpers;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;

namespace DataLayer.Repositories.Implementation
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(MasterContext context) : base(context)
        {
        }

        public IEnumerable<Notification> GetAllMotionNotifications()
        {
            return GetAll().Where(x => x.NotificationTypeId == (int) NotificationTypes.MotionDetection);
        }

        public IEnumerable<Notification> GetAllSensorNotifications()
        {
            return GetAll().Where(x => x.NotificationTypeId == (int) NotificationTypes.SensorReading);
        }
    }
}