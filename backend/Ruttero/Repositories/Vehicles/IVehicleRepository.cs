using System.Threading.Tasks;
using Ruttero.Models;

namespace Ruttero.Interfaces.Repositories
{
    public interface IVehicleRepository
    {
        Task<bool> ExistsByLicensePlateAsync(string licensePlate);
        Task CreateAsync(Vehicle vehicle);
        Task<Vehicle?> GetByIdAsync(int id);
        Task UpdateAsync(Vehicle vehicle);
    }
}


