using Microsoft.EntityFrameworkCore;
using Ruttero.Models;

namespace Ruttero.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Fare> Fares { get; set; }
        public DbSet<Trip> Trips { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set enum TripStatus as string to store in DB
            modelBuilder.Entity<Trip>()
                .Property(t => t.Status)
                .HasConversion(
                    v => v.ToString(), // Enum to string (store)
                    v => (TripStatus)Enum.Parse(typeof(TripStatus), v)  // String to enum (read)
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}