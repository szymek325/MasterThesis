using System;
using System.Reflection;
using DataLayer.Implementation.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataLayer.Implementation
{
    public class MasterContext : DbContext
    {
        public MasterContext(DbContextOptions<MasterContext> options)
            : base(options)
        {
        }

        public DbSet<FaceRecognitionJob> FaceRecognitionJobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FaceRecognitionJob>().ToTable(nameof(FaceRecognitionJob));
        }
    }


    public class TemporaryDbContextFactory : IDesignTimeDbContextFactory<MasterContext>
    {
        public MasterContext CreateDbContext(string[] args)
        {
            Console.WriteLine("dupa1");
            var builder = new DbContextOptionsBuilder<MasterContext>();
            builder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=TestDb;Trusted_Connection=True;ConnectRetryCount=0",
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(MasterContext).GetTypeInfo().Assembly.GetName().Name));
            return new MasterContext(builder.Options);
        }

        MasterContext IDesignTimeDbContextFactory<MasterContext>.CreateDbContext(string[] args)
        {
            Console.WriteLine("dupa2");
            Console.WriteLine(string.Join(' ',args));
            var builder = new DbContextOptionsBuilder<MasterContext>();
            builder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=TestDb;Trusted_Connection=True;ConnectRetryCount=0",
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(MasterContext).GetTypeInfo().Assembly.GetName().Name));
            return new MasterContext(builder.Options);
        }
    }
}