using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            InitializeStatuses(modelBuilder);
            InitializeNeuralNetworkTypes(modelBuilder);
            InitializeDetectionTypes(modelBuilder);
            InitializeAttachmentTypes(modelBuilder);
            InitializeNotificationTypes(modelBuilder);
            InitializeNotificationSettings(modelBuilder);
        }

        private static void InitializeDetectionTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DetectionType>().HasData(
                new DetectionType
                {
                    Id=1,
                    Name = "dnn"
                },
                new DetectionType
                {
                    Id = 2,
                    Name = "haar"
                },
                new DetectionType
                {
                    Id = 3,
                    Name = "azure"
                }
            );
        }

        private static void InitializeNeuralNetworkTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NeuralNetworkType>().HasData(
                new NeuralNetworkType
                {
                    Id = 1,
                    Name = "LBPH"
                },
                new NeuralNetworkType
                {
                    Id = 2,
                    Name = "Eigen"
                },
                new NeuralNetworkType
                {
                    Id = 3,
                    Name = "Fisher"
                },
                new NeuralNetworkType
                {
                    Id = 4,
                    Name = "AzureLargeGroup"
                }
            );
        }

        private static void InitializeStatuses(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(
                new Status
                {
                    Id = 1,
                    Name = "New"
                },
                new Status
                {
                    Id = 2,
                    Name = "In Progress"
                },
                new Status
                {
                    Id = 3,
                    Name = "Completed"
                },
                new Status
                {
                    Id = 4,
                    Name = "Error"
                }
            );
        }

        private static void InitializeAttachmentTypes(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ImageAttachmentType>().HasData(
                new ImageAttachmentType
                {
                    Id = 1,
                    Name = "Detection"
                },
                new ImageAttachmentType
                {
                    Id = 2,
                    Name = "DetectionResult"
                },
                new ImageAttachmentType
                {
                    Id = 3,
                    Name = "Recognition"
                },
                new ImageAttachmentType
                {
                    Id = 4,
                    Name = "Person"
                },
                new ImageAttachmentType
                {
                    Id = 5,
                    Name = "Motion"
                }
            );
        }

        private static void InitializeNotificationTypes(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotificationType>().HasData(
                new NotificationType
                {
                    Id = 1,
                    Name = "Sensor Reading"
                },
                new NotificationType
                {
                    Id = 2,
                    Name = "Motion Detection"
                }
            );
        }

        private static void InitializeNotificationSettings(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotificationSettings>().HasData(
                new NotificationSettings
                {
                    Id = 1,
                    Name = "Temperature",
                    Min = 15,
                    Max = 30
                },
                new NotificationSettings
                {
                    Id = 2,
                    Name = "Humidity",
                    Min = 30,
                    Max = 60
                }
            );
        }
    }
}