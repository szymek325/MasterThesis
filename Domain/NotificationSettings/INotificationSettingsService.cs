using System.Collections.Generic;
using Domain.NotificationSettings.DTO;

namespace Domain.NotificationSettings
{
    public interface INotificationSettingsService
    {
        IEnumerable<SettingOutput> GetAllSettings();
    }
}