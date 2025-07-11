using Ruttero.Models;
using Ruttero.Dtos.Trips;
using Ruttero.Interfaces.Services;
using Ruttero.Interfaces.Repositories;

namespace Ruttero.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;

        public TripService(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }
        public async Task<TripResponseDto> CreateTripAsync(CreateTripRequestDto requestDto, int userId)
        {
            var newTrip = new Trip
            {
                DriverId = requestDto.DriverId,
                VehicleId = requestDto.VehicleId,
                FareId = requestDto.FareId,
                Origin = requestDto.Origin,
                Destination = requestDto.Destination,
                Date = requestDto.Date,
                CreatedBy = userId,
                Status = TripStatus.pending,
                CreatedAt = DateTime.UtcNow
            };

            await _tripRepository.CreateAsync(newTrip);

            return new TripResponseDto
            {
                Success = true,
                Message = "El viaje se ha registrado exitosamente."
            };
        }

        public async Task<TripResponseDto> UpdateTripAsync(UpdateTripRequestDto requestDto)
        {
            var trip = await _tripRepository.GetByIdAsync(requestDto.Id);
            if (trip == null)
            {
                return new TripResponseDto
                {
                    Success = false,
                    Message = "Viaje no encontrado."
                };
            }

            if (trip.IsActive == requestDto.IsActive)
            {
                return new TripResponseDto
                {
                    Success = false,
                    Message = $"El viaje ya está {(trip.IsActive ? "activo" : "inactivo")}."
                };
            }

            trip.IsActive = requestDto.IsActive;
            await _tripRepository.UpdateAsync(trip);

            return new TripResponseDto
            {
                Success = true,
                Message = $"El viaje fue {(trip.IsActive ? "reactivado" : "desactivado")} exitosamente."
            };
        }
    }
}