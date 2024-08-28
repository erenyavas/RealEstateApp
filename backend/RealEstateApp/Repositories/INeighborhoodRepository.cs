using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public interface INeighborhoodRepository
    {
        Task<IEnumerable<Neighborhood>> GetAllAsync();
        Task<Neighborhood> GetByIdAsync(int id);
        Task<IEnumerable<Neighborhood>> GetNeighborhoodsByDistrictIdAsync(int districtId);
        Task AddAsync(Neighborhood neighborhood);
        Task UpdateAsync(Neighborhood neighborhood);
        Task DeleteAsync(Neighborhood neighborhood);
    }
}
