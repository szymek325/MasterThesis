using DataLayer.Entities;
using DataLayer.Repositories.Base;
using DataLayer.Repositories.Interface;

namespace DataLayer.Repositories.Implementation
{
    public class NotificationSettingsRepository : GenericRepository<NotificationSettings>,
        INotificationSettingsRepository
    {
        public NotificationSettingsRepository(MasterContext context) : base(context)
        {
        }
    }
}