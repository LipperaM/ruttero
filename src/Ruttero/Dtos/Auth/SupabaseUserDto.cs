using System.Text.Json.Serialization;

namespace Ruttero.Dtos.Auth
{
    public class SupabaseUserDto
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
        
        [JsonPropertyName("user_metadata")]
        public Dictionary<string, object>? UserMetadata { get; set; }
        
        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; set; }
        
        [JsonPropertyName("updated_at")]
        public string? UpdatedAt { get; set; }
    }
}
