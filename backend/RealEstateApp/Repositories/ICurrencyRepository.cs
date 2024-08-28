using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency>> GetAllAsync();
        Task<Currency> GetByIdAsync(int id);
        Task AddAsync(Currency currency);
        Task UpdateAsync(Currency currency);
        Task DeleteAsync(Currency currency);
    }
}
