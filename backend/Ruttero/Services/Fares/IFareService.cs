using Ruttero.Dtos.Fares;
using Ruttero.Models;

namespace Ruttero.Interfaces.Services
{
    public interface IFareService
    {
        Task<FareResponseDto> CreateFareAsync(CreateFareRequestDto requestDto, int userId);
        Task<FareResponseDto> UpdateFareAsync(UpdateFareRequestDto requestDto);
    }
}