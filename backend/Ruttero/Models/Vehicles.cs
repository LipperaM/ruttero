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

        public int? DriverId { get; set; }  // FK nullable

        public bool IsActive { get; set; } = true;

        public DateTime? CreatedAt { get; set; }

        // Navegación hacia Driver
        public Driver? Driver { get; set; }
    }
}
