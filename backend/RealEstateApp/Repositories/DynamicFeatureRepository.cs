using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public class DynamicFeatureRepository : IDynamicFeatureRepository
    {
        private readonly ApplicationDbContext _context;

        public DynamicFeatureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DynamicFeature>> GetAllAsync()
        {
            return await _context.DynamicFeatures.ToListAsync();
        }

        public async Task<DynamicFeature> GetByIdAsync(int id)
        {
            return await _context.DynamicFeatures.FindAsync(id);
        }

        public async Task AddAsync(DynamicFeature dynamicFeature)
        {
            _context.DynamicFeatures.Add(dynamicFeature);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DynamicFeature dynamicFeature)
        {
            _context.DynamicFeatures.Update(dynamicFeature);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(DynamicFeature dynamicFeature)
        {
            _context.DynamicFeatures.Remove(dynamicFeature);
            await _context.SaveChangesAsync();
        }
    }
}
