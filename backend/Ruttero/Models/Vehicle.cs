using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruttero.Models
{
    [Table("vehicles")]
    public class Vehicle
    {
        public int Id { get; set; }

        [Column("license_plate")]
        public string LicensePlate { get; set; } = null!;

        public string? Model { get; set; }

        public string? Brand { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_by")]
        public int CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public User User { get; set; } = null!;

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
    }
}
