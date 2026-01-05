namespace Ruttero.Dtos.Auth
{
    public class SupabaseSignInResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public SupabaseUserDto? User { get; set; }
        public SupabaseAuthResponseDto? Session { get; set; }
    }
}
