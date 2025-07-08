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

        public bool IsActive { get; set; } = true;  // tinyint(1) DEFAULT '1' => bool con default true

        public DateTime? CreatedAt { get; set; }    // timestamp NULL DEFAU
    }
}