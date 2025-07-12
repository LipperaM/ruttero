using Ruttero.Dtos.Trips;
using Ruttero.Models;

namespace Ruttero.Interfaces.Services
{
    public interface ITripService
    {
        Task<TripResponseDto> CreateTripAsync(CreateTripRequestDto requestDto, int userId);
        Task<TripResponseDto> UpdateTripAsync(UpdateTripRequestDto requestDto);
        Task<List<GetAllTripsDto>> GetAllTripsAsync(int userId);

    }
}