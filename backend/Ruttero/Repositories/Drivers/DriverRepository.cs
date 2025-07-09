using System.Threading.Tasks;
using Ruttero.Models;
using Ruttero.Data;
using Microsoft.EntityFrameworkCore;
using Ruttero.Interfaces.Repositories;


namespace Ruttero.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly AppDbContext _context;

        public DriverRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByNationalIdAsync(string nationalId)
        {
            return await _context.Drivers.AnyAsync(n => n.NationalId == nationalId);
        }
        public async Task CreateAsync(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
        }

        public async Task<Driver?> GetByIdAsync(int Id)
        {
            return await _context.Drivers.FirstOrDefaultAsync(d => d.Id == Id);
        }

        public async Task UpdateAsync(Driver driver)
        {
            _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();
        }
    }
}
