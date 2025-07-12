using System.Threading.Tasks;
using Ruttero.Models;

namespace Ruttero.Interfaces.Repositories
{
    public interface ITripRepository
    {
        Task CreateAsync(Trip trip);
        Task<Trip?> GetByIdAsync(int id);
        Task UpdateAsync(Trip trip);
        Task<List<Trip>> GetAllTripsAsync(int userId);

    }
}
