using System.Linq;
using DataLayer.Entities;

namespace DataLayer
{
    public static class DbInitializer
    {
        public static void Seed(MasterContext context)
        {
            InitializeStatuses(context);
            InitializeNeuralNetworkTypes(context);
            InitializeDetectionTypes(context);
            InitializeAttachmentTypes(context);
            InitializeNotificationSettings(context);

            context.SaveChanges();
        }

        private static void InitializeDetectionTypes(MasterContext context)
        {
            if (context.DetectionTypes.Any()) return;
            context.DetectionTypes.Add(new DetectionType
            {
                Name = "dnn"
            });
            context.DetectionTypes.Add(new DetectionType
            {
                Name = "haar"
            });
            context.DetectionTypes.Add(new DetectionType
            {
                Name = "azure"
            });
        }

        private static void InitializeNeuralNetworkTypes(MasterContext context)
        {
            if (context.NeuralNetworkTypes.Any()) return;
            context.NeuralNetworkTypes.Add(new NeuralNetworkType
            {
                Name = "LBPH"
            });
            context.NeuralNetworkTypes.Add(new NeuralNetworkType
            {
                Name = "Eigen"
            });
            context.NeuralNetworkTypes.Add(new NeuralNetworkType
            {
                Name = "Fisher"
            });
            context.NeuralNetworkTypes.Add(new NeuralNetworkType
            {
                Name = "AzureLargeGroup"
            });
        }

        private static void InitializeStatuses(MasterContext context)
        {
            if (context.Statuses.Any()) return;
            context.Statuses.Add(new Status
            {
                Name = "New"
            });
            context.Statuses.Add(new Status
            {
                Name = "In Progress"
            });
            context.Statuses.Add(new Status
            {
                Name = "Completed"
            });
            context.Statuses.Add(new Status
            {
                Name = "Error"
            });
        }

        private static void InitializeAttachmentTypes(MasterContext context)
        {
            if (context.ImageAttachmentTypes.Any()) return;
            context.ImageAttachmentTypes.Add(new ImageAttachmentType
            {
                Name = "Detection"
            });
            context.ImageAttachmentTypes.Add(new ImageAttachmentType
            {
                Name = "DetectionResult"
            });
            context.ImageAttachmentTypes.Add(new ImageAttachmentType
            {
                Name = "Recognition"
            });
            context.ImageAttachmentTypes.Add(new ImageAttachmentType
            {
                Name = "Person"
            });
            context.ImageAttachmentTypes.Add(new ImageAttachmentType
            {
                Name = "Motion"
            });
        }

        private static void InitializeNotificationTypes(MasterContext context)
        {
            if (context.NotificationTypes.Any()) return;
            context.NotificationTypes.Add(new NotificationType
            {
                Name = "Sensor Reading"
            });
            context.NotificationTypes.Add(new NotificationType
            {
                Name = "Motion Detection"
            });
        }

        private static void InitializeNotificationSettings(MasterContext context)
        {
            if (context.NotificationSettings.Any()) return;
            context.NotificationSettings.Add(new NotificationSettings
            {
                Name = "Temperature",
                Min = 15,
                Max = 30
            });
            context.NotificationSettings.Add(new NotificationSettings
            {
                Name = "Humidity",
                Min = 30,
                Max = 60
            });
        }
    }
}