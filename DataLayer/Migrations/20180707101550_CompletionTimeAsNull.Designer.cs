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
    [Migration("20180707101550_CompletionTimeAsNull")]
    partial class CompletionTimeAsNull
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.Entities.Detection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CompletionTime");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("DnnFaces");

                    b.Property<int>("HaarFaces");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int?>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("Detection");
                });

            modelBuilder.Entity("DataLayer.Entities.DetectionImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int?>("DetectionId");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<string>("Thumbnail");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("DetectionId");

                    b.ToTable("DetectionImage");
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
                        .ValueGeneratedOnAdd();

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

            modelBuilder.Entity("DataLayer.Entities.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("DataLayer.Entities.PersonImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int?>("PersonId");

                    b.Property<string>("Thumbnail");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonImage");
                });

            modelBuilder.Entity("DataLayer.Entities.Recognition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

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

            modelBuilder.Entity("DataLayer.Entities.RecognitionImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int?>("RecognitionId");

                    b.Property<string>("Thumbnail");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("RecognitionId");

                    b.ToTable("RecognitionImage");
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

            modelBuilder.Entity("DataLayer.Entities.Detection", b =>
                {
                    b.HasOne("DataLayer.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");
                });

            modelBuilder.Entity("DataLayer.Entities.DetectionImage", b =>
                {
                    b.HasOne("DataLayer.Entities.Detection", "Detection")
                        .WithMany("Images")
                        .HasForeignKey("DetectionId");
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

            modelBuilder.Entity("DataLayer.Entities.PersonImage", b =>
                {
                    b.HasOne("DataLayer.Entities.Person", "Person")
                        .WithMany("Images")
                        .HasForeignKey("PersonId");
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

            modelBuilder.Entity("DataLayer.Entities.RecognitionImage", b =>
                {
                    b.HasOne("DataLayer.Entities.Recognition", "Recognition")
                        .WithMany("Images")
                        .HasForeignKey("RecognitionId");
                });
#pragma warning restore 612, 618
        }
    }
}
