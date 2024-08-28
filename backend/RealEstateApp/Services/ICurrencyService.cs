using RealEstateApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public interface ICurrencyService
    {
        Task<IEnumerable<CurrencyDto>> GetAllAsync();
        Task<CurrencyDto> GetByIdAsync(int id);
        Task AddAsync(CurrencyDto currencyDto);
        Task UpdateAsync(CurrencyDto currencyDto);
        Task DeleteAsync(int id);
    }
}
