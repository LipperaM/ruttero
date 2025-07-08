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

    }
}