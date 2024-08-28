using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly ApplicationDbContext _context;

        public FeatureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Feature>> GetAllAsync()
        {
            return await _context.Features.ToListAsync();
        }

        public async Task<Feature> GetByIdAsync(int id)
        {
            return await _context.Features.FindAsync(id);
        }
        public async Task<IEnumerable<Feature>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Features
                .Where(f => f.FeatureCategoryId == categoryId)
                .ToListAsync();
        }

        public async Task AddAsync(Feature feature)
        {
            await _context.Features.AddAsync(feature);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Feature feature)
        {
            _context.Features.Update(feature);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var feature = await _context.Features.FindAsync(id);
            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();
        }
    }
}
