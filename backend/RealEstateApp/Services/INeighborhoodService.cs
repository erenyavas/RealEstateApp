using RealEstateApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public interface INeighborhoodService
    {
        Task<IEnumerable<NeighborhoodDto>> GetAllAsync();
        Task<NeighborhoodDto> GetByIdAsync(int id);
        Task<IEnumerable<NeighborhoodDto>> GetNeighborhoodsByDistrictIdAsync(int districtId);
        Task AddAsync(NeighborhoodDto neighborhoodDto);
        Task UpdateAsync(NeighborhoodDto neighborhoodDto);
        Task DeleteAsync(int id);
    }
}
