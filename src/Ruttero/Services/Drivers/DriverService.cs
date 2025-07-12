using Ruttero.Models;
using Ruttero.Dtos.Drivers;
using Ruttero.Interfaces.Services;
using Ruttero.Interfaces.Repositories;

namespace Ruttero.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }
        public async Task<DriverResponseDto> CreateDriverAsync(CreateDriverRequestDto requestDto, int userId)
        {
            if (await _driverRepository.ExistsByNationalIdAsync(requestDto.NationalId))
            {
                return new DriverResponseDto
                {
                    Success = false,
                    Message = "El conductor ya se encuentra registrado."
                };
            }

            var newDriver = new Driver
            {
                Name = requestDto.Name,
                Surname = requestDto.Surname,
                NationalId = requestDto.NationalId,
                CreatedBy = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _driverRepository.CreateAsync(newDriver);

            return new DriverResponseDto
            {
                Success = true,
                Message = "El conductor se ha registrado exitosamente."
            };
        }

        public async Task<DriverResponseDto> UpdateDriverAsync(UpdateDriverRequestDto requestDto)
        {
            var driver = await _driverRepository.GetByIdAsync(requestDto.Id);
            if (driver == null)
            {
                return new DriverResponseDto
                {
                    Success = false,
                    Message = "Conductor no encontrado."
                };
            }

            if (driver.IsActive == requestDto.IsActive)
            {
                return new DriverResponseDto
                {
                    Success = false,
                    Message = $"El conductor ya está {(driver.IsActive ? "activo" : "inactivo")}."
                };
            }

            driver.IsActive = requestDto.IsActive;
            await _driverRepository.UpdateAsync(driver);

            return new DriverResponseDto
            {
                Success = true,
                Message = $"El conductor fue {(driver.IsActive ? "reactivado" : "desactivado")} exitosamente."
            };
        }

        public async Task<List<GetAllDriversDto>> GetAllDriversAsync(int userId)
        {
            var drivers = await _driverRepository.GetAllDriversAsync(userId);

            var responseDto = drivers.Select(d => new GetAllDriversDto
            {
                Id = d.Id,
                Name = d.Name,
                Surname = d.Surname,
                NationalId = d.NationalId
            }).ToList();

            return responseDto;
        }
    }
}