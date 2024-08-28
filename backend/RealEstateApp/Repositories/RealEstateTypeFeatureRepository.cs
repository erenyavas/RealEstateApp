using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public class RealEstateTypeFeatureRepository : IRealEstateTypeFeatureRepository
    {
        private readonly ApplicationDbContext _context;

        public RealEstateTypeFeatureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RealEstateTypeFeature>> GetAllAsync()
        {
            return await _context.RealEstateTypeFeatures
                .Include(rt => rt.RealEstateType)
                .Include(rt => rt.Feature)
                .ToListAsync();
        }

        public async Task<RealEstateTypeFeature> GetByIdAsync(int realEstateTypeId, int featureId)
        {
            return await _context.RealEstateTypeFeatures
                .Include(rt => rt.RealEstateType)
                .Include(rt => rt.Feature)
                .FirstOrDefaultAsync(rt => rt.RealEstateTypeId == realEstateTypeId && rt.FeatureId == featureId);
        }

        public async Task AddAsync(RealEstateTypeFeature realEstateTypeFeature)
        {
            _context.RealEstateTypeFeatures.Add(realEstateTypeFeature);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RealEstateTypeFeature realEstateTypeFeature)
        {
            _context.RealEstateTypeFeatures.Remove(realEstateTypeFeature);
            await _context.SaveChangesAsync();
        }
    }
}
