using System.Linq;
using DataLayer.Entities;
using DataLayer.Entities.Common;
using DataLayer.Entities.ManyToManyHelper;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class MasterContext : DbContext
    {
        public MasterContext(DbContextOptions<MasterContext> options)
            : base(options)
        {
        }

        public DbSet<SensorsReading> SensorsReadings { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Detection> Detections { get; set; }
        public DbSet<DetectionResult> DetectionResults { get; set; }
        public DbSet<DetectionRectangle> DetectionRectangles { get; set; }
        public DbSet<DetectionType> DetectionTypes { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Recognition> Recognitions { get; set; }
        public DbSet<NeuralNetwork> NeuralNetworks { get; set; }
        public DbSet<NeuralNetworkFile> NeuralNetworkFiles { get; set; }
        public DbSet<NeuralNetworkType> NeuralNetworkTypes { get; set; }
        public DbSet<NeuralNetworkPerson> NeuralNetworkPeople { get; set; }
        public DbSet<ImageAttachment> ImageAttachments { get; set; }
        public DbSet<ImageAttachmentType> ImageAttachmentTypes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<NotificationSettings> NotificationSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NeuralNetworkPerson>()
                .HasKey(bc => new {bc.PersonId, NeuralNetworkRequestId = bc.NeuralNetworkId});

            modelBuilder.Entity<NeuralNetworkPerson>()
                .HasOne(bc => bc.NeuralNetwork)
                .WithMany("NeuralNetworkPeople");

            modelBuilder.Entity<NeuralNetworkPerson>()
                .HasOne(bc => bc.Person)
                .WithMany("NeuralNetworkPeople");

            //less important stuff
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(t => t.ClrType.IsSubclassOf(typeof(EntityBase))))
                modelBuilder.Entity(
                    entityType.Name,
                    x =>
                    {
                        x.Property("CreationTime")
                            .HasDefaultValueSql("getutcdate()");
                    });

            modelBuilder.Entity<SensorsReading>().ToTable(nameof(SensorsReading));
            modelBuilder.Entity<Status>().ToTable(nameof(Status));
            modelBuilder.Entity<Detection>().ToTable(nameof(Detection));
            modelBuilder.Entity<DetectionResult>().ToTable(nameof(DetectionResult));
            modelBuilder.Entity<DetectionRectangle>().ToTable(nameof(DetectionRectangle));
            modelBuilder.Entity<DetectionType>().ToTable(nameof(DetectionType));
            modelBuilder.Entity<Person>().ToTable(nameof(Person));
            modelBuilder.Entity<Recognition>().ToTable(nameof(Recognition));
            modelBuilder.Entity<NeuralNetwork>().ToTable(nameof(NeuralNetwork));
            modelBuilder.Entity<NeuralNetworkFile>().ToTable(nameof(NeuralNetworkFile));
            modelBuilder.Entity<NeuralNetworkType>().ToTable(nameof(NeuralNetworkType));
            modelBuilder.Entity<ImageAttachment>().ToTable(nameof(ImageAttachment));
            modelBuilder.Entity<ImageAttachmentType>().ToTable(nameof(ImageAttachmentType));
            modelBuilder.Entity<Notification>().ToTable(nameof(Notification));
            modelBuilder.Entity<NotificationType>().ToTable(nameof(NotificationType));
            modelBuilder.Entity<NotificationSettings>().ToTable(nameof(Entities.NotificationSettings));
        }
    }
}