using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public class NeighborhoodRepository : INeighborhoodRepository
    {
        private readonly ApplicationDbContext _context;

        public NeighborhoodRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Neighborhood>> GetAllAsync()
        {
            return await _context.Neighborhoods.ToListAsync();
        }

        public async Task<Neighborhood> GetByIdAsync(int id)
        {
            return await _context.Neighborhoods.FindAsync(id);
        }

        public async Task<IEnumerable<Neighborhood>> GetNeighborhoodsByDistrictIdAsync(int districtId)
        {
            return await _context.Neighborhoods
                .Where(n => n.DistrictId == districtId)
                .ToListAsync();
        }

        public async Task AddAsync(Neighborhood neighborhood)
        {
            await _context.Neighborhoods.AddAsync(neighborhood);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Neighborhood neighborhood)
        {
            _context.Neighborhoods.Update(neighborhood);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Neighborhood neighborhood)
        {
            _context.Neighborhoods.Remove(neighborhood);
            await _context.SaveChangesAsync();
        }
    }
}
