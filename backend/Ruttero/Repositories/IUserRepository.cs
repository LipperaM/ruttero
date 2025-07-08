using System.Threading.Tasks;
using Ruttero.Models;

namespace Ruttero.Repositories
{
    public interface IUserRepository
    {
        Task<bool> ExistsByUsernameAsync(string username);
        Task<bool> ExistsByEmailAsync(string email);
        Task CreateAsync(User user);
        Task<User?> GetByUsernameAsync(string username);
    }
}
