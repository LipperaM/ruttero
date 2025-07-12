using System.Threading.Tasks;
using Ruttero.Models;

namespace Ruttero.Interfaces.Repositories
{
    public interface IFareRepository
    {
        Task<bool> ExistsByIdAsync(int id);
        Task CreateAsync(Fare fare);
        Task<Fare?> GetByIdAsync(int id);
        Task UpdateAsync(Fare fare);
        Task<List<Fare>> GetAllFaresAsync(int userId);
    }
}
