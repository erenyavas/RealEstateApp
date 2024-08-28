using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public class RealEstateTypeRepository : IRealEstateTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public RealEstateTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RealEstateType>> GetAllAsync()
        {
            return await _context.RealEstateTypes
        .Include(rt => rt.RealEstateTypeFeatures)
            .ThenInclude(rtf => rtf.Feature)
        .Include(rt => rt.RealEstateTypeFeatureCategories)
            .ThenInclude(rtfc => rtfc.FeatureCategory)
                .ThenInclude(fc => fc.Features)
        .ToListAsync();
        }

        public async Task<RealEstateType> GetByIdAsync(int id)
        {
            return await _context.RealEstateTypes
        .Include(rt => rt.RealEstateTypeFeatures)
            .ThenInclude(rtf => rtf.Feature)
        .Include(rt => rt.RealEstateTypeFeatureCategories)
            .ThenInclude(rtfc => rtfc.FeatureCategory)
                .ThenInclude(fc => fc.Features)
        .FirstOrDefaultAsync(rt => rt.Id == id);
        }

        public async Task AddAsync(RealEstateType realEstateType)
        {
            await _context.RealEstateTypes.AddAsync(realEstateType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RealEstateType realEstateType)
        {
            _context.RealEstateTypes.Update(realEstateType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RealEstateType realEstateType)
        {
            _context.RealEstateTypes.Remove(realEstateType);
            await _context.SaveChangesAsync();
        }
    }
}
