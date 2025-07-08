using Ruttero.Dtos.Drivers;

namespace Ruttero.Interfaces.Services
{
    public interface IDriverService
    {
        Task<DriverResponseDto> CreateDriverAsync(CreateDriverRequestDto requestDto);
        Task<DriverResponseDto> UpdateDriverAsync(UpdateDriverRequestDto requestDto);
    }
}