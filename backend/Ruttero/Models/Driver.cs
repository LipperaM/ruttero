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

        [Column("national_id")]
        public string NationalId { get; set; } = null!;

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_by")]
        public int CreatedBy { get; set; } // user id
        
        [ForeignKey("CreatedBy")]
        public User User { get; set; } = null!;
        
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
    }
}
