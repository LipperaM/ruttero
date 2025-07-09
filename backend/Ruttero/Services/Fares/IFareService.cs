using Ruttero.Dtos.Fares;

namespace Ruttero.Interfaces.Services
{
    public interface IFareService
    {
        Task<FareResponseDto> CreateFareAsync(CreateFareRequestDto requestDto);
        Task<FareResponseDto> UpdateFareAsync(UpdateFareRequestDto requestDto);
    }
}