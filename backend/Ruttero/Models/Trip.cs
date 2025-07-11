using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruttero.Models
{
    public enum TripStatus
    {
        pending,
        active,
        completed,
        cancelled
    }

    [Table("trips")]
    public class Trip
    {
        public int Id { get; set; }

        [Column("created_by")]
        public int CreatedBy { get; set; } // user id
        [ForeignKey("CreatedBy")]
        public User User { get; set; } = null!;

        [Column("driver_id")]
        public int DriverId { get; set; }
        [ForeignKey("DriverId")]
        public Driver Driver { get; set; } = null!;

        [Column("vehicle_id")]
        public int VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; } = null!;

        [Column("fare_id")]
        public int FareId { get; set; }
        [ForeignKey("FareId")]
        public Fare Fare { get; set; } = null!;

        public string Origin { get; set; } = null!;

        public string Destination { get; set; } = null!;

        public DateTime Date { get; set; }

        public TripStatus Status { get; set; } = TripStatus.pending;

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;  // default true
    }
}
