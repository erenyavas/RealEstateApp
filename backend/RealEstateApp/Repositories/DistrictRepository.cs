using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public class DistrictRepository : IDistrictRepository
    {
        private readonly ApplicationDbContext _context;

        public DistrictRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<District>> GetAllAsync()
        {
            return await _context.Districts.ToListAsync();
        }

        public async Task<District> GetByIdAsync(int id)
        {
            return await _context.Districts.FindAsync(id);
        }

        public async Task<IEnumerable<District>> GetDistrictsByCityIdAsync(int cityId)
        {
            return await _context.Districts
                                 .Where(d => d.CityId == cityId)
                                 .ToListAsync();
        }


        public async Task AddAsync(District district)
        {
            await _context.Districts.AddAsync(district);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(District district)
        {
            _context.Districts.Update(district);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(District district)
        {
            _context.Districts.Remove(district);
            await _context.SaveChangesAsync();
        }
    }
}
