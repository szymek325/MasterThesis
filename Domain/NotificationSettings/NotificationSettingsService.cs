using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataLayer.Repositories.Interface;
using Domain.NotificationSettings.DTO;
using Microsoft.Extensions.Logging;

namespace Domain.NotificationSettings
{
    public class NotificationSettingsService : INotificationSettingsService
    {
        private readonly ILogger<NotificationSettingsService> logger;
        private readonly IMapper mapper;
        private readonly INotificationSettingsRepository settingsRepository;

        public NotificationSettingsService(ILogger<NotificationSettingsService> logger,
            INotificationSettingsRepository settingsRepository, IMapper mapper)
        {
            this.logger = logger;
            this.settingsRepository = settingsRepository;
            this.mapper = mapper;
        }

        public IEnumerable<SettingOutput> GetAllSettings()
        {
            var settings = settingsRepository.GetAll().ToList();
            var settingsDto = mapper.Map<IEnumerable<SettingOutput>>(settings);
            return settingsDto;
        }
    }
}