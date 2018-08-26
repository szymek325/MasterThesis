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
                    Name = "dnn"
                },
                new DetectionType
                {
                    Name = "haar"
                },
                new DetectionType
                {
                    Name = "azure"
                }
            );
        }

        private static void InitializeNeuralNetworkTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NeuralNetworkType>().HasData(
                new NeuralNetworkType
                {
                    Name = "LBPH"
                },
                new NeuralNetworkType
                {
                    Name = "Eigen"
                },
                new NeuralNetworkType
                {
                    Name = "Fisher"
                },
                new NeuralNetworkType
                {
                    Name = "AzureLargeGroup"
                }
            );
        }

        private static void InitializeStatuses(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(
                new Status
                {
                    Name = "New"
                },
                new Status
                {
                    Name = "In Progress"
                },
                new Status
                {
                    Name = "Completed"
                },
                new Status
                {
                    Name = "Error"
                }
            );
        }

        private static void InitializeAttachmentTypes(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(
                new ImageAttachmentType
                {
                    Name = "Detection"
                },
                new ImageAttachmentType
                {
                    Name = "DetectionResult"
                },
                new ImageAttachmentType
                {
                    Name = "Recognition"
                },
                new ImageAttachmentType
                {
                    Name = "Person"
                },
                new ImageAttachmentType
                {
                    Name = "Motion"
                }
            );
        }

        private static void InitializeNotificationTypes(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(
                new NotificationType
                {
                    Name = "Sensor Reading"
                },
                new NotificationType
                {
                    Name = "Motion Detection"
                }
            );
        }

        private static void InitializeNotificationSettings(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(
                new NotificationSettings
                {
                    Name = "Temperature",
                    Min = 15,
                    Max = 30
                },
                new NotificationSettings
                {
                    Name = "Humidity",
                    Min = 30,
                    Max = 60
                }
            );
        }
    }
}