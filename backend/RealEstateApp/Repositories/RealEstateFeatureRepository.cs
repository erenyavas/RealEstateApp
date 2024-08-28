using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public class RealEstateFeatureRepository : IRealEstateFeatureRepository
    {
        private readonly ApplicationDbContext _context;

        public RealEstateFeatureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RealEstateFeature>> GetAllAsync()
        {
            return await _context.RealEstateFeatures.ToListAsync();
        }

        public async Task<RealEstateFeature> GetByIdAsync(int realEstateId, int featureId)
        {
            return await _context.RealEstateFeatures
                .FirstOrDefaultAsync(rf => rf.RealEstateId == realEstateId && rf.FeatureId == featureId);
        }

        public async Task AddAsync(RealEstateFeature realEstateFeature)
        {
            await _context.RealEstateFeatures.AddAsync(realEstateFeature);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int realEstateId, int featureId)
        {
            var realEstateFeature = await _context.RealEstateFeatures
                .FirstOrDefaultAsync(rf => rf.RealEstateId == realEstateId && rf.FeatureId == featureId);
            _context.RealEstateFeatures.Remove(realEstateFeature);
            await _context.SaveChangesAsync();
        }
    }
}
