using System.Text;
using System.Text.Json;
using Ruttero.Dtos.Auth;

namespace Ruttero.Services.Supabase
{
    public interface ISupabaseAuthService
    {
        Task<SupabaseSignUpResponseDto> SignUpAsync(string email, string password, Dictionary<string, object>? metadata = null);
        Task<SupabaseSignInResponseDto> SignInAsync(string email, string password);
        Task<SupabaseUserDto?> GetUserAsync(string accessToken);
    }

    public class SupabaseAuthService : ISupabaseAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _supabaseUrl;

        public SupabaseAuthService(IConfiguration configuration)
        {
            _supabaseUrl = Environment.GetEnvironmentVariable("SUPABASE_URL") 
                ?? configuration["Supabase:Url"] 
                ?? "http://auth:9999";
            
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_supabaseUrl)
            };
        }

        public async Task<SupabaseSignUpResponseDto> SignUpAsync(string email, string password, Dictionary<string, object>? metadata = null)
        {
            var payload = new
            {
                email,
                password,
                data = metadata ?? new Dictionary<string, object>()
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("/signup", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new SupabaseSignUpResponseDto
                {
                    Success = false,
                    Message = $"Error al registrar usuario: {responseContent}"
                };
            }

            var result = JsonSerializer.Deserialize<SupabaseAuthResponseDto>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return new SupabaseSignUpResponseDto
            {
                Success = true,
                Message = "Usuario registrado exitosamente",
                User = result?.User,
                Session = result
            };
        }

        public async Task<SupabaseSignInResponseDto> SignInAsync(string email, string password)
        {
            var payload = new
            {
                email,
                password
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("/token?grant_type=password", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new SupabaseSignInResponseDto
                {
                    Success = false,
                    Message = $"Error al iniciar sesión: {responseContent}"
                };
            }

            var result = JsonSerializer.Deserialize<SupabaseAuthResponseDto>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return new SupabaseSignInResponseDto
            {
                Success = true,
                Message = "Inicio de sesión exitoso",
                User = result?.User,
                Session = result
            };
        }

        public async Task<SupabaseUserDto?> GetUserAsync(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync("/user");
            
            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SupabaseUserDto>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
