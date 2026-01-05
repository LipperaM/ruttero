using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ruttero.Dtos.Auth;
using Ruttero.Services.Supabase;
using System.Security.Claims;

namespace Ruttero.Controllers
{
    [ApiController]
    [Route("auth")]
    public class SupabaseAuthController : ControllerBase
    {
        private readonly ISupabaseAuthService _supabaseAuthService;

        public SupabaseAuthController(ISupabaseAuthService supabaseAuthService)
        {
            _supabaseAuthService = supabaseAuthService;
        }

        // Register new user with Supabase Auth
        [HttpPost("signup")]
        public async Task<ActionResult> SignUp([FromBody] SupabaseSignUpRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { message = "Email y contraseña son requeridos" });
            }

            var metadata = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(request.Username))
            {
                metadata["username"] = request.Username;
            }

            var result = await _supabaseAuthService.SignUpAsync(request.Email, request.Password, metadata);

            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            // Return new token and user info
            return Ok(new
            {
                message = result.Message,
                user = result.User,
                session = new
                {
                    access_token = result.Session?.AccessToken,
                    token_type = result.Session?.TokenType,
                    expires_in = result.Session?.ExpiresIn,
                    refresh_token = result.Session?.RefreshToken
                }
            });
        }

        // Login endpoint
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] SupabaseLoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { message = "Email y contraseña son requeridos" });
            }

            var result = await _supabaseAuthService.SignInAsync(request.Email, request.Password);

            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            // Return new token and user info
            return Ok(new
            {
                message = result.Message,
                user = result.User,
                session = new
                {
                    access_token = result.Session?.AccessToken,
                    token_type = result.Session?.TokenType,
                    expires_in = result.Session?.ExpiresIn,
                    refresh_token = result.Session?.RefreshToken
                }
            });
        }

        // Obtain user profile
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult> GetProfile()
        {
            // Validated JWT token by authentication middleware
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { message = "Usuario no autenticado" });
            }

            // Return user profile info
            return Ok(new
            {
                id = userId,
                email = email,
                claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }
    }
}
