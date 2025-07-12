using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ruttero.Models
{
    [Table("users")]
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        [Column("password_hash")]
        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; } = "client";

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
    }
}
