using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public class RealEstateTypeFeatureCategoryRepository : IRealEstateTypeFeatureCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public RealEstateTypeFeatureCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RealEstateTypeFeatureCategory>> GetAllAsync()
        {
            return await _context.RealEstateTypeFeatureCategories.ToListAsync();
        }

        public async Task<RealEstateTypeFeatureCategory> GetByIdAsync(int realEstateTypeId, int featureCategoryId)
        {
            return await _context.RealEstateTypeFeatureCategories
                .FirstOrDefaultAsync(rtf => rtf.RealEstateTypeId == realEstateTypeId && rtf.FeatureCategoryId == featureCategoryId);
        }

        public async Task AddAsync(RealEstateTypeFeatureCategory realEstateTypeFeatureCategory)
        {
            await _context.RealEstateTypeFeatureCategories.AddAsync(realEstateTypeFeatureCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int realEstateTypeId, int featureCategoryId)
        {
            var realEstateTypeFeatureCategory = await _context.RealEstateTypeFeatureCategories
                .FirstOrDefaultAsync(rtf => rtf.RealEstateTypeId == realEstateTypeId && rtf.FeatureCategoryId == featureCategoryId);
            _context.RealEstateTypeFeatureCategories.Remove(realEstateTypeFeatureCategory);
            await _context.SaveChangesAsync();
        }
    }
}
