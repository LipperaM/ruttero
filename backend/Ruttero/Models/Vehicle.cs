using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruttero.Models
{
    [Table("vehicles")]
    public class Vehicle
    {
        public int Id { get; set; }

        public string LicensePlate { get; set; } = null!;

        public string? Model { get; set; }

        public string? Brand { get; set; }

        public int? DriverId { get; set; } 

        public bool IsActive { get; set; } = true;

        public DateTime? CreatedAt { get; set; }

        public Driver? Driver { get; set; }
    }
}
