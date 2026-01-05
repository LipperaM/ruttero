using System.Text.Json.Serialization;

namespace Ruttero.Dtos.Auth
{
    public class SupabaseAuthResponseDto
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
        
        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }
        
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
        
        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }
        
        [JsonPropertyName("user")]
        public SupabaseUserDto? User { get; set; }
    }
}
