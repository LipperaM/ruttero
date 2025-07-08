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

        public string Password_Hash { get; set; } = null!;

        public string Role { get; set; } = "client";

        public DateTime? Created_At { get; set; }
    }
}
