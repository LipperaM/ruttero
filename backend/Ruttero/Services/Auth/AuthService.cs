using Ruttero.Dtos.Auth;
using Ruttero.Data;
using Microsoft.EntityFrameworkCore;
using Ruttero.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Ruttero.Repositories;
using Ruttero.Interfaces.Services;
using Ruttero.Interfaces.Repositories;

namespace Ruttero.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<SignUpResponseDto> SignUpAsync(SignUpRequestDto requestDto)
        {
            if (await _userRepository.ExistsByUsernameAsync(requestDto.Username))
            {
                return new SignUpResponseDto
                {
                    Success = false,
                    Message = "El nombre de usuario ya está en uso."
                };
            }

            if (await _userRepository.ExistsByEmailAsync(requestDto.Email))
            {
                return new SignUpResponseDto
                {
                    Success = false,
                    Message = "El email ya está registrado."
                };
            }

            var password_Hash = BCrypt.Net.BCrypt.HashPassword(requestDto.Password);

            var newUser = new User
            {
                Username = requestDto.Username,
                Email = requestDto.Email,
                PasswordHash = password_Hash,
                Role = "client",
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.CreateAsync(newUser);

            return new SignUpResponseDto
            {
                Success = true,
                Message = "Registro existoso."
            };
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto requestDto)
        {
            var user = await _userRepository.GetByUsernameAsync(requestDto.Username);
            if (user == null)
                return null;

            // Verify password hash
            if (!BCrypt.Net.BCrypt.Verify(requestDto.Password, user.PasswordHash))
                return null;

            // Generate JWT claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new LoginResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}
