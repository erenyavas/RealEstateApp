using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ApplicationDbContext _context;
        
        public CurrencyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            return await _context.Currencies.ToListAsync();
        }

        public async Task<Currency> GetByIdAsync(int id)
        {
            return await _context.Currencies.FindAsync(id);
        }

        public async Task AddAsync(Currency currency)
        {
            await _context.Currencies.AddAsync(currency);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Currency currency)
        {
            _context.Currencies.Update(currency);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Currency currency)
        {
            _context.Currencies.Remove(currency);
            await _context.SaveChangesAsync();
        }
    }
}
