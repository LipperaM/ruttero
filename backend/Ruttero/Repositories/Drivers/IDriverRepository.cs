using System.Threading.Tasks;
using Ruttero.Models;

namespace Ruttero.Interfaces.Repositories
{
    public interface IDriverRepository
    {
        Task<bool> ExistsByNationalIdAsync(string nationalId);
        Task CreateAsync(Driver driver);
        Task<Driver?> GetByIdAsync(int Id);
        Task UpdateAsync(Driver driver);
    }
}






