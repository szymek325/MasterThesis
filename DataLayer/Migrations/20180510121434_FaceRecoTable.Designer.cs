﻿// <auto-generated />
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DataLayer.Migrations
{
    [DbContext(typeof(MasterContext))]
    [Migration("20180510121434_FaceRecoTable")]
    partial class FaceRecoTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.Entities.FaceDetection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("DnnFaces");

                    b.Property<string>("Guid")
                        .IsRequired();

                    b.Property<int>("HaarFaces");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int?>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("FaceDetection");
                });

            modelBuilder.Entity("DataLayer.Entities.FaceRecognition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description");

                    b.Property<int?>("FileId");

                    b.Property<string>("Guid")
                        .IsRequired();

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int?>("NeuralNetworkId");

                    b.Property<int?>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("FileId")
                        .IsUnique()
                        .HasFilter("[FileId] IS NOT NULL");

                    b.HasIndex("NeuralNetworkId");

                    b.HasIndex("StatusId");

                    b.ToTable("FaceRecognitions");
                });

            modelBuilder.Entity("DataLayer.Entities.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("FaceDetectionGuid");

                    b.Property<string>("FaceRecognitionId");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<string>("PersonGuid");

                    b.Property<string>("Thumbnail");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("FaceDetectionGuid");

                    b.HasIndex("PersonGuid");

                    b.ToTable("File");
                });

            modelBuilder.Entity("DataLayer.Entities.NeuralNetwork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int?>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("NeuralNetworks");
                });

            modelBuilder.Entity("DataLayer.Entities.NeuralNetworkPerson", b =>
                {
                    b.Property<int>("PersonId");

                    b.Property<int>("NeuralNetworkId");

                    b.HasKey("PersonId", "NeuralNetworkId");

                    b.HasIndex("NeuralNetworkId");

                    b.ToTable("NeuralNetworkPerson");
                });

            modelBuilder.Entity("DataLayer.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description");

                    b.Property<string>("Guid")
                        .IsRequired();

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("DataLayer.Entities.SensorsReading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

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
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("DataLayer.Entities.FaceDetection", b =>
                {
                    b.HasOne("DataLayer.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("DataLayer.Entities.FaceRecognition", b =>
                {
                    b.HasOne("DataLayer.Entities.File", "File")
                        .WithOne("FaceRecognition")
                        .HasForeignKey("DataLayer.Entities.FaceRecognition", "FileId");

                    b.HasOne("DataLayer.Entities.NeuralNetwork", "NeuralNetwork")
                        .WithMany()
                        .HasForeignKey("NeuralNetworkId");

                    b.HasOne("DataLayer.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("DataLayer.Entities.File", b =>
                {
                    b.HasOne("DataLayer.Entities.FaceDetection", "FaceDetection")
                        .WithMany("Files")
                        .HasForeignKey("FaceDetectionGuid")
                        .HasPrincipalKey("Guid");

                    b.HasOne("DataLayer.Entities.Person", "Person")
                        .WithMany("Files")
                        .HasForeignKey("PersonGuid")
                        .HasPrincipalKey("Guid");
                });

            modelBuilder.Entity("DataLayer.Entities.NeuralNetwork", b =>
                {
                    b.HasOne("DataLayer.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("DataLayer.Entities.NeuralNetworkPerson", b =>
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
#pragma warning restore 612, 618
        }
    }
}
