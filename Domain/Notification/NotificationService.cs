using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Helpers;
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
        private readonly INotificationRepository notificationRepository;

        public NotificationService(ILogger<NotificationService> logger, INotificationRepository notificationRepository,
            IFilesDomainService filesService, IMapper mapper)
        {
            this.logger = logger;
            this.notificationRepository = notificationRepository;
            this.filesService = filesService;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<NotificationOutput>> GetAllNotifications()
        {
            var notifications = notificationRepository.GetAllWithIncluded().ToList();
            try
            {
                foreach (var notify in notifications)
                    if (string.IsNullOrWhiteSpace(notify.Image.Thumbnail)
                        && notify.NotificationTypeId == (int) NotificationTypes.MotionDetection)
                        await filesService.GetThumbnail(notify.Image);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception when trying to obtain thumbnails for notifications");
            }

            var requests = mapper.Map<IEnumerable<NotificationOutput>>(notifications);
            return requests;
        }

        //public Task<NeuralNetworkRequest> GetById(int id)
        //{
        //    var neuralNetwork = nnRepo.GetById(id);
        //    var output = mapper.Map<NeuralNetworkRequest>(neuralNetwork);
        //    return Task.FromResult(output);
        //}

        //public Task<IEnumerable<AllNeuralNetworksOutput>> GetAll()
        //{
        //    var neuralNetworks = nnRepo.GetAllNeuralNetworksWithDependencies().ToList();
        //    var output = mapper.Map<IEnumerable<AllNeuralNetworksOutput>>(neuralNetworks);
        //    return Task.FromResult(output);
        //}
    }
}