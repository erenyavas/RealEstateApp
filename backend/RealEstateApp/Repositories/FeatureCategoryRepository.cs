using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public class FeatureCategoryRepository : IFeatureCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public FeatureCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FeatureCategory>> GetAllAsync()
        {
            return await _context.FeatureCategories.ToListAsync();
        }

        public async Task<FeatureCategory> GetByIdAsync(int id)
        {
            return await _context.FeatureCategories.FindAsync(id);
        }

        public async Task AddAsync(FeatureCategory featureCategory)
        {
            await _context.FeatureCategories.AddAsync(featureCategory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FeatureCategory featureCategory)
        {
            _context.FeatureCategories.Update(featureCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var featureCategory = await _context.FeatureCategories.FindAsync(id);
            _context.FeatureCategories.Remove(featureCategory);
            await _context.SaveChangesAsync();
        }
    }
}
