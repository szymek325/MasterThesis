﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(MasterContext))]
    partial class MasterContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.Entities.Detection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CompletionTime");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Detection");
                });

            modelBuilder.Entity("DataLayer.Entities.DetectionRectangle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Area");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("DetectionResultId");

                    b.Property<int>("EndX");

                    b.Property<int>("EndY");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("StartX");

                    b.Property<int>("StartY");

                    b.HasKey("Id");

                    b.HasIndex("DetectionResultId");

                    b.ToTable("DetectionRectangle");
                });

            modelBuilder.Entity("DataLayer.Entities.DetectionResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("DetectionId");

                    b.Property<int>("DetectionTypeId");

                    b.Property<DateTime?>("ModifiedDate");

                    b.HasKey("Id");

                    b.HasIndex("DetectionId");

                    b.HasIndex("DetectionTypeId");

                    b.ToTable("DetectionResult");
                });

            modelBuilder.Entity("DataLayer.Entities.DetectionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("DetectionType");

                    b.HasData(
                        new { Id = 1, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "dnn" },
                        new { Id = 2, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "haar" },
                        new { Id = 3, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "azure" }
                    );
                });

            modelBuilder.Entity("DataLayer.Entities.ImageAttachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int?>("DetectionId");

                    b.Property<int?>("DetectionResultId");

                    b.Property<int>("ImageAttachmentTypeId");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int?>("NotificationId");

                    b.Property<int?>("PersonId");

                    b.Property<int?>("RecognitionId");

                    b.Property<string>("Thumbnail");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("DetectionId")
                        .IsUnique()
                        .HasFilter("[DetectionId] IS NOT NULL");

                    b.HasIndex("DetectionResultId")
                        .IsUnique()
                        .HasFilter("[DetectionResultId] IS NOT NULL");

                    b.HasIndex("ImageAttachmentTypeId");

                    b.HasIndex("NotificationId")
                        .IsUnique()
                        .HasFilter("[NotificationId] IS NOT NULL");

                    b.HasIndex("PersonId");

                    b.HasIndex("RecognitionId")
                        .IsUnique()
                        .HasFilter("[RecognitionId] IS NOT NULL");

                    b.ToTable("ImageAttachment");
                });

            modelBuilder.Entity("DataLayer.Entities.ImageAttachmentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ImageAttachmentType");

                    b.HasData(
                        new { Id = 1, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "Detection" },
                        new { Id = 2, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "DetectionResult" },
                        new { Id = 3, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "Recognition" },
                        new { Id = 4, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "Person" },
                        new { Id = 5, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "Motion" }
                    );
                });

            modelBuilder.Entity("DataLayer.Entities.ManyToManyHelper.NeuralNetworkPerson", b =>
                {
                    b.Property<int>("PersonId");

                    b.Property<int>("NeuralNetworkId");

                    b.HasKey("PersonId", "NeuralNetworkId");

                    b.HasIndex("NeuralNetworkId");

                    b.ToTable("NeuralNetworkPeople");
                });

            modelBuilder.Entity("DataLayer.Entities.NeuralNetwork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CompletionTime");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int?>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("NeuralNetwork");
                });

            modelBuilder.Entity("DataLayer.Entities.NeuralNetworkFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalData");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int>("NeuralNetworkId");

                    b.Property<int>("NeuralNetworkTypeId");

                    b.HasKey("Id");

                    b.HasIndex("NeuralNetworkId");

                    b.HasIndex("NeuralNetworkTypeId");

                    b.ToTable("NeuralNetworkFile");
                });

            modelBuilder.Entity("DataLayer.Entities.NeuralNetworkType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("NeuralNetworkType");

                    b.HasData(
                        new { Id = 1, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "LBPH" },
                        new { Id = 2, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "Eigen" },
                        new { Id = 3, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "Fisher" },
                        new { Id = 4, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "AzureLargeGroup" }
                    );
                });

            modelBuilder.Entity("DataLayer.Entities.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Message");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("NotificationTypeId");

                    b.HasKey("Id");

                    b.HasIndex("NotificationTypeId");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("DataLayer.Entities.NotificationSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("Max");

                    b.Property<int>("Min");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("NotificationSettings");

                    b.HasData(
                        new { Id = 1, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Max = 30, Min = 15, Name = "Temperature" },
                        new { Id = 2, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Max = 60, Min = 30, Name = "Humidity" }
                    );
                });

            modelBuilder.Entity("DataLayer.Entities.NotificationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("NotificationType");

                    b.HasData(
                        new { Id = 1, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "Sensor Reading" },
                        new { Id = 2, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "Motion Detection" }
                    );
                });

            modelBuilder.Entity("DataLayer.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("DataLayer.Entities.Recognition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CompletionTime");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int?>("NeuralNetworkId");

                    b.Property<int?>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("NeuralNetworkId");

                    b.HasIndex("StatusId");

                    b.ToTable("Recognition");
                });

            modelBuilder.Entity("DataLayer.Entities.RecognitionResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments");

                    b.Property<int>("Confidence");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("IdentifiedPersonId");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("NeuralNetworkFileId");

                    b.Property<int>("RecognitionId");

                    b.HasKey("Id");

                    b.HasIndex("NeuralNetworkFileId");

                    b.HasIndex("RecognitionId");

                    b.ToTable("RecognitionResult");
                });

            modelBuilder.Entity("DataLayer.Entities.SensorsReading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("Humidity");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("Temperature");

                    b.HasKey("Id");

                    b.ToTable("SensorsReading");
                });

            modelBuilder.Entity("DataLayer.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Status");

                    b.HasData(
                        new { Id = 1, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "New" },
                        new { Id = 2, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "In Progress" },
                        new { Id = 3, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "Completed" },
                        new { Id = 4, CreationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), Name = "Error" }
                    );
                });

            modelBuilder.Entity("DataLayer.Entities.Detection", b =>
                {
                    b.HasOne("DataLayer.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.DetectionRectangle", b =>
                {
                    b.HasOne("DataLayer.Entities.DetectionResult", "DetectionResult")
                        .WithMany("FaceRectangles")
                        .HasForeignKey("DetectionResultId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.DetectionResult", b =>
                {
                    b.HasOne("DataLayer.Entities.Detection", "Detection")
                        .WithMany("Results")
                        .HasForeignKey("DetectionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Entities.DetectionType", "DetectionType")
                        .WithMany()
                        .HasForeignKey("DetectionTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.ImageAttachment", b =>
                {
                    b.HasOne("DataLayer.Entities.Detection", "Detection")
                        .WithOne("Image")
                        .HasForeignKey("DataLayer.Entities.ImageAttachment", "DetectionId");

                    b.HasOne("DataLayer.Entities.DetectionResult", "DetectionResult")
                        .WithOne("Image")
                        .HasForeignKey("DataLayer.Entities.ImageAttachment", "DetectionResultId");

                    b.HasOne("DataLayer.Entities.ImageAttachmentType", "ImageAttachmentType")
                        .WithMany()
                        .HasForeignKey("ImageAttachmentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Entities.Notification", "Notification")
                        .WithOne("Image")
                        .HasForeignKey("DataLayer.Entities.ImageAttachment", "NotificationId");

                    b.HasOne("DataLayer.Entities.Person", "Person")
                        .WithMany("Images")
                        .HasForeignKey("PersonId");

                    b.HasOne("DataLayer.Entities.Recognition", "Recognition")
                        .WithOne("Image")
                        .HasForeignKey("DataLayer.Entities.ImageAttachment", "RecognitionId");
                });

            modelBuilder.Entity("DataLayer.Entities.ManyToManyHelper.NeuralNetworkPerson", b =>
                {
                    b.HasOne("DataLayer.Entities.NeuralNetwork", "NeuralNetwork")
                        .WithMany("NeuralNetworkPeople")
                        .HasForeignKey("NeuralNetworkId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Entities.Person", "Person")
                        .WithMany("NeuralNetworkPeople")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.NeuralNetwork", b =>
                {
                    b.HasOne("DataLayer.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("DataLayer.Entities.NeuralNetworkFile", b =>
                {
                    b.HasOne("DataLayer.Entities.NeuralNetwork", "NeuralNetwork")
                        .WithMany("Files")
                        .HasForeignKey("NeuralNetworkId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Entities.NeuralNetworkType", "NeuralNetworkType")
                        .WithMany()
                        .HasForeignKey("NeuralNetworkTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.Notification", b =>
                {
                    b.HasOne("DataLayer.Entities.NotificationType", "NotificationType")
                        .WithMany()
                        .HasForeignKey("NotificationTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataLayer.Entities.Recognition", b =>
                {
                    b.HasOne("DataLayer.Entities.NeuralNetwork", "NeuralNetwork")
                        .WithMany()
                        .HasForeignKey("NeuralNetworkId");

                    b.HasOne("DataLayer.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("DataLayer.Entities.RecognitionResult", b =>
                {
                    b.HasOne("DataLayer.Entities.NeuralNetworkFile", "NeuralNetworkFile")
                        .WithMany()
                        .HasForeignKey("NeuralNetworkFileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataLayer.Entities.Recognition", "Recognition")
                        .WithMany("RecognitionResults")
                        .HasForeignKey("RecognitionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
