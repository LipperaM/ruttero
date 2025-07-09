using Ruttero.Models;
using Ruttero.Dtos.Fares;
using Ruttero.Interfaces.Services;
using Ruttero.Interfaces.Repositories;

namespace Ruttero.Services
{
    public class FareService : IFareService
    {
        private readonly IFareRepository _fareRepository;

        public FareService(IFareRepository fareRepository)
        {
            _fareRepository = fareRepository;
        }
        public async Task<FareResponseDto> CreateFareAsync(CreateFareRequestDto requestDto)
        {
            var newFare = new Fare
            {
                Description = requestDto.Description,
                Price = requestDto.Price,
                CreatedAt = DateTime.UtcNow
            };

            await _fareRepository.CreateAsync(newFare);

            return new FareResponseDto
            {
                Success = true,
                Message = "La tarifa se ha registrado exitosamente."
            };
        }

        public async Task<FareResponseDto> UpdateFareAsync(UpdateFareRequestDto requestDto)
        {
            var fare = await _fareRepository.GetByIdAsync(requestDto.Id);
            if (fare == null)
            {
                return new FareResponseDto
                {
                    Success = false,
                    Message = "Tarifa no encontrada."
                };
            }

            if (fare.IsActive == requestDto.IsActive)
            {
                return new FareResponseDto
                {
                    Success = false,
                    Message = $"La tarifa ya está {(fare.IsActive ? "activa" : "inactiva")}."
                };
            }

            fare.IsActive = requestDto.IsActive;
            await _fareRepository.UpdateAsync(fare);

            return new FareResponseDto
            {
                Success = true,
                Message = $"La tarifa fue {(fare.IsActive ? "reactivada" : "desactivada")} exitosamente."
            };
        }
    }
}