using API.Domain.Models;
using API.Services.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence
{
    public class DataContext: IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<AccelerometerMeasurement> AccelerometerMeasurements { get; set; }
        public DbSet<GyroscopeMeasurement> GyroscopeMeasurements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seeding a User role to AspNetRoles table
            builder.Entity<IdentityRole>().HasData(new IdentityRole {Id = "5c5e174e-3b0e-446f-86af-483d56fd7210", Name = Role.User, NormalizedName = Role.User.ToUpper() });
        }
    }
}