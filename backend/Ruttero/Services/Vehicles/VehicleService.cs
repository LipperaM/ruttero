using Ruttero.Models;
using Ruttero.Dtos.Vehicles;
using Ruttero.Interfaces.Services;
using Ruttero.Interfaces.Repositories;

namespace Ruttero.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        public async Task<VehicleResponseDto> CreateVehicleAsync(CreateVehicleRequestDto requestDto, int userId)
        {
            if (await _vehicleRepository.ExistsByLicensePlateAsync(requestDto.LicensePlate))
            {
                return new VehicleResponseDto
                {
                    Success = false,
                    Message = "El vehiculo ya se encuentra registrado."
                };
            }

            var newVehicle = new Vehicle
            {
                LicensePlate = requestDto.LicensePlate,
                Model = requestDto.Model,
                Brand = requestDto.Brand,
                CreatedBy = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _vehicleRepository.CreateAsync(newVehicle);

            return new VehicleResponseDto
            {
                Success = true,
                Message = "El vehiculo se ha registrado exitosamente."
            };
        }

        public async Task<VehicleResponseDto> UpdateVehicleAsync(UpdateVehicleRequestDto requestDto)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(requestDto.Id);
            if (vehicle == null)
            {
                return new VehicleResponseDto
                {
                    Success = false,
                    Message = "Vehiculo no encontrado."
                };
            }

            if (vehicle.IsActive == requestDto.IsActive)
            {
                return new VehicleResponseDto
                {
                    Success = false,
                    Message = $"El vehiculo ya está {(vehicle.IsActive ? "activo" : "inactivo")}."
                };
            }

            vehicle.IsActive = requestDto.IsActive;
            await _vehicleRepository.UpdateAsync(vehicle);

            return new VehicleResponseDto
            {
                Success = true,
                Message = $"El Vehiculo fue {(vehicle.IsActive ? "reactivado" : "desactivado")} exitosamente."
            };
        }
    }
}