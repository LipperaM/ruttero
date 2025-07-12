using System.Threading.Tasks;
using Ruttero.Models;
using Ruttero.Data;
using Microsoft.EntityFrameworkCore;
using Ruttero.Interfaces.Repositories;


namespace Ruttero.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly AppDbContext _context;

        public TripRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Trip trip)
        {
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
        }

        public async Task<Trip?> GetByIdAsync(int id)
        {
            return await _context.Trips.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task UpdateAsync(Trip trip)
        {
            _context.Trips.Update(trip);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Trip>> GetAllTripsAsync(int userId)
        {
            return await _context.Trips
                .Include(t => t.Driver)
                .Include(t => t.Vehicle)
                .Include(t => t.Fare)
                .Where(t => t.CreatedBy == userId && t.IsActive)
                .ToListAsync();
        }
    }
}