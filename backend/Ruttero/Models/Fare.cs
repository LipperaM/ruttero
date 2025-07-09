using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruttero.Models
{
    [Table("fares")]
    public class Fare
    {
        public int Id { get; set; }

        public string? Description { get; set; }  // varchar(255) DEFAULT NULL => nullable string

        public decimal Price { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;  // default true

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }    // timestamp NULL DEFAULT
    }
}