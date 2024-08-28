using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public class RealEstateFeatureValueRepository : IRealEstateFeatureValueRepository
    {
        private readonly ApplicationDbContext _context;

        public RealEstateFeatureValueRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RealEstateFeatureValue>> GetAllAsync()
        {
            return await _context.RealEstateFeatureValues
                .Include(fv => fv.RealEstate)
                .Include(fv => fv.Feature)
                .ToListAsync();
        }

        public async Task<RealEstateFeatureValue> GetByIdAsync(int id)
        {
            return await _context.RealEstateFeatureValues
                .Include(fv => fv.RealEstate)
                .Include(fv => fv.Feature)
                .FirstOrDefaultAsync(fv => fv.Id == id);
        }

        public async Task AddAsync(RealEstateFeatureValue featureValue)
        {
            _context.RealEstateFeatureValues.Add(featureValue);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RealEstateFeatureValue featureValue)
        {
            _context.RealEstateFeatureValues.Update(featureValue);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RealEstateFeatureValue featureValue)
        {
            _context.RealEstateFeatureValues.Remove(featureValue);
            await _context.SaveChangesAsync();
        }
    }
}
