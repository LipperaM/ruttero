using System.Threading.Tasks;
using Ruttero.Models;

namespace Ruttero.Interfaces.Repositories
{
    public interface IFareRepository
    {
        Task<bool> ExistsByIdAsync(int Id);
        Task CreateAsync(Fare fare);
        Task<Fare?> GetByIdAsync(int Id);
        Task UpdateAsync(Fare fare);
    }
}
