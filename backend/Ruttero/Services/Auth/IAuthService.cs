using Ruttero.Dtos.Auth;
using System.Threading.Tasks;

namespace Ruttero.Interfaces.Services
{
    public interface IAuthService
    {
        Task<SignUpResponseDto> SignUpAsync(SignUpRequestDto requestDto);
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto requestDto);
    }
}