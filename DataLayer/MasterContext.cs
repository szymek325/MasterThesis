using System.Reflection;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataLayer
{
    public class MasterContext : DbContext
    {
        public MasterContext(DbContextOptions<MasterContext> options)
            : base(options)
        {
        }

        public DbSet<FaceRecognitionJob> FaceRecognitionJobs { get; set; }
        public DbSet<SensorsReading> SensorsReadings { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<FaceDetection> FaceDetections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FaceRecognitionJob>().ToTable(nameof(FaceRecognitionJob));
            modelBuilder.Entity<SensorsReading>().ToTable(nameof(SensorsReading));
            modelBuilder.Entity<Status>().ToTable(nameof(Status));
            modelBuilder.Entity<FaceDetection>().ToTable(nameof(FaceDetection));
        }
    }

    //public class TemporaryDbContextFactory : IDesignTimeDbContextFactory<MasterContext>
    //{
    //    MasterContext IDesignTimeDbContextFactory<MasterContext>.CreateDbContext(string[] args)
    //    {
    //        var builder = new DbContextOptionsBuilder<MasterContext>();
    //        builder.UseSqlServer(
    //            "Data Source=den1.mssql6.gear.host;Initial Catalog=masterthesisdb;Integrated Security=False;User ID=masterthesisdb;Password=Zp9P?Q!ezuXH;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
    //            optionsBuilder =>
    //                optionsBuilder.MigrationsAssembly(typeof(MasterContext).GetTypeInfo().Assembly.GetName().Name));
    //        return new MasterContext(builder.Options);
    //    }
    //}
}