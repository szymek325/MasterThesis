using System.Threading.Tasks;
using Domain.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebRazor.Models.Notification;

namespace WebRazor.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationService notificationService;
        private readonly ILogger<NotificationController> logger;

        public NotificationController(INotificationService notificationService, ILogger<NotificationController> logger)
        {
            this.notificationService = notificationService;
            this.logger = logger;
        }

        public async Task<IActionResult> AllNotifications()
        {
            var notifications = await notificationService.GetAllNotifications();
            var model = new NotificationsViewModel(notifications);
            return View(model);
        }
    }
}