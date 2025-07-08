using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruttero.Models
{
    public enum TripStatus
    {
        Pending,
        InProgress,
        Completed,
        Cancelled
    }

    [Table("trips")]
    public class Trip
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int DriverId { get; set; }
        public Driver Driver { get; set; } = null!;

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;

        public int FareId { get; set; }
        public Fare Fare { get; set; } = null!;

        public string Origin { get; set; } = null!;

        public string Destination { get; set; } = null!;

        public DateTime Date { get; set; }

        public TripStatus Status { get; set; } = TripStatus.Pending;

        public DateTime? CreatedAt { get; set; }
    }
}
