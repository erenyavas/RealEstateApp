using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public class RealEstateStatusRepository : IRealEstateStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public RealEstateStatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RealEstateStatus>> GetAllAsync()
        {
            return await _context.RealEstateStatuses.ToListAsync();
        }

        public async Task<RealEstateStatus> GetByIdAsync(int id)
        {
            return await _context.RealEstateStatuses.FindAsync(id);
        }

        public async Task AddAsync(RealEstateStatus realEstateStatus)
        {
            await _context.RealEstateStatuses.AddAsync(realEstateStatus);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RealEstateStatus realEstateStatus)
        {
            _context.RealEstateStatuses.Update(realEstateStatus);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RealEstateStatus realEstateStatus)
        {
            _context.RealEstateStatuses.Remove(realEstateStatus);
            await _context.SaveChangesAsync();
        }
    }
}
