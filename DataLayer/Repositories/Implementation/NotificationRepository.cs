using System.Collections.Generic;
using System.Linq;
using DataLayer.Entities;
using DataLayer.Helpers;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementation
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(MasterContext context) : base(context)
        {
        }

        public IEnumerable<Notification> GetAllWithIncluded()
        {
            return GetAll().Include(x => x.Image);
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