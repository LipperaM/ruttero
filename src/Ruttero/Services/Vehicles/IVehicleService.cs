using Ruttero.Dtos.Vehicles;

namespace Ruttero.Interfaces.Services
{
    public interface IVehicleService
    {
        Task<VehicleResponseDto> CreateVehicleAsync(CreateVehicleRequestDto requestDto, int userId);
        Task<VehicleResponseDto> UpdateVehicleAsync(UpdateVehicleRequestDto requestDto);
        Task<List<GetAllVehiclesDto>> GetAllVehiclesAsync(int userId);
    }
}