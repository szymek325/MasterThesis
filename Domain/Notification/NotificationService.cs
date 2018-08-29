using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Repositories.Interface;
using Domain.Files;
using Domain.Notification.DTO;
using Microsoft.Extensions.Logging;

namespace Domain.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly IFilesDomainService filesService;
        private readonly ILogger<NotificationService> logger;
        private readonly IMapper mapper;
        private readonly IMovementRepository movementRepository;
        private readonly INotificationRepository notificationRepository;

        public NotificationService(IFilesDomainService filesService, ILogger<NotificationService> logger,
            IMapper mapper,
            INotificationRepository notificationRepository, IMovementRepository movementRepository)
        {
            this.filesService = filesService;
            this.logger = logger;
            this.mapper = mapper;
            this.notificationRepository = notificationRepository;
            this.movementRepository = movementRepository;
        }

        public async Task<IEnumerable<NotificationOutput>> GetAllNotifications()
        {
            try
            {
                var notifications = notificationRepository.GetAll().ToList();
                var movements = movementRepository.GetAllWithImages().ToList();
                await GetLinksToFiles(movements);
                var mappedNotifications = mapper.Map<IEnumerable<NotificationOutput>>(notifications);
                var mappedMovements = mapper.Map<IEnumerable<NotificationOutput>>(movements);
                var allNotifactions = new List<NotificationOutput>();
                allNotifactions.AddRange(mappedMovements);
                allNotifactions.AddRange(mappedNotifications);
                var orderByDescending = allNotifactions.OrderByDescending(x => x.CreationTime);
                return orderByDescending;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception when loading notifications");
                throw;
            }
        }

        private async Task GetLinksToFiles(List<Movement> movements)
        {
            try
            {
                foreach (var movement in movements)
                {
                    if (movement != null)
                        continue;
                    if (string.IsNullOrWhiteSpace(movement.Image?.Thumbnail))
                        await filesService.GetThumbnail(movement.Image);
                    if (string.IsNullOrWhiteSpace(movement.Image?.Url))
                        await filesService.GetLinkToFile(movement.Image);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception when trying to obtain thumbnails for notifications");
            }
        }
    }
}