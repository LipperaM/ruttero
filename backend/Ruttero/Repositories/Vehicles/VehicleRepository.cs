using System.Threading.Tasks;
using Ruttero.Models;
using Ruttero.Data;
using Microsoft.EntityFrameworkCore;
using Ruttero.Interfaces.Repositories;


namespace Ruttero.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext _context;

        public VehicleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByLicensePlateAsync(string licensePlate)
        {
            return await _context.Vehicles.AnyAsync(l => l.LicensePlate == licensePlate);
        }
        public async Task CreateAsync(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<Vehicle?> GetByIdAsync(int id)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }
    }
}