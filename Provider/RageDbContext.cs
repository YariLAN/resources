using DbModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Provider
{
    public class RageDbContext : DbContext
    {
        public DbSet<LoginModel> LoginModels { get; set; }

        public DbSet<UserModel> UserModels { get; set; }

        public DbSet<VehicleModel> VehicleModels { get; set; }

        public DbSet<VehiclePrice> VehiclePrice { get; set; }

        public DbSet<Blips> Blips { get; set; }

        public DbSet<TypesBlips> TypesBlips { get; set; }

        // private string ConnectionString =>
        //     new ConfigurationBuilder()
        //     .AddJsonFile("appsettings.json")
        //     .Build()
        //     .GetConnectionString("DefaultConnection");

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(ConnectionString);

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=RageDb; User Id=admin;Password=Post");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("DbModels"));
        }
    }
}
