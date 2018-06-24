using System.Linq;
using DataLayer.Entities;
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
        public DbSet<FaceDetection> FaceDetections { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<NeuralNetwork> NeuralNetworks { get; set; }
        public DbSet<FaceRecognition> FaceRecognitions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<NeuralNetworkPerson>()
                .HasKey(bc => new { bc.PersonId, NeuralNetworkRequestId = bc.NeuralNetworkId });

            modelBuilder.Entity<NeuralNetworkPerson>()
                .HasOne(bc => bc.NeuralNetwork)
                .WithMany("NeuralNetworkPeople");

            modelBuilder.Entity<NeuralNetworkPerson>()
                .HasOne(bc => bc.Person)
                .WithMany("NeuralNetworkPeople");

            //less important stuff
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(t => t.ClrType.IsSubclassOf(typeof(EntityBase))))
            {
                modelBuilder.Entity(
                    entityType.Name,
                    x =>
                    {
                        x.Property("CreationTime")
                            .HasDefaultValueSql("getutcdate()");
                    });
            }

            modelBuilder.Entity<SensorsReading>().ToTable(nameof(SensorsReading));
            modelBuilder.Entity<Status>().ToTable(nameof(Status));
            modelBuilder.Entity<FaceDetection>().ToTable(nameof(FaceDetection));
            modelBuilder.Entity<File>().ToTable(nameof(File));
            modelBuilder.Entity<Person>().ToTable(nameof(Person));

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