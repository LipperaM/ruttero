using System.Threading.Tasks;
using Ruttero.Models;
using Ruttero.Data;
using Microsoft.EntityFrameworkCore;
using Ruttero.Interfaces.Repositories;


namespace Ruttero.Repositories
{
    public class FareRepository : IFareRepository
    {
        private readonly AppDbContext _context;

        public FareRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await _context.Fares.AnyAsync(n => n.Id == id);
        }
        public async Task CreateAsync(Fare fare)
        {
            _context.Fares.Add(fare);
            await _context.SaveChangesAsync();
        }

        public async Task<Fare?> GetByIdAsync(int id)
        {
            return await _context.Fares.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task UpdateAsync(Fare fare)
        {
            _context.Fares.Update(fare);
            await _context.SaveChangesAsync();
        }
    }
}
