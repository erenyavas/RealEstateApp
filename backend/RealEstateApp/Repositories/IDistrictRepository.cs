using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public interface IDistrictRepository
    {
        Task<IEnumerable<District>> GetAllAsync();
        Task<District> GetByIdAsync(int id);
        Task<IEnumerable<District>> GetDistrictsByCityIdAsync(int cityId);
        Task AddAsync(District district);
        Task UpdateAsync(District district);
        Task DeleteAsync(District district);
    }
}
