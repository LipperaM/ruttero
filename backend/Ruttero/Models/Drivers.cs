using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruttero.Models
{
    
    [Table("drivers")]
    public class Driver
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public string NationalId { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public DateTime? CreatedAt { get; set; }
    }
}
