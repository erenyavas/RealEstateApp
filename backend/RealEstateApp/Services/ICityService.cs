using RealEstateApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public interface ICityService
    {
        Task<IEnumerable<CityDto>> GetAllAsync();
        Task<CityDto> GetByIdAsync(int id);
        Task AddAsync(CityDto cityDto);
        Task UpdateAsync(CityDto cityDto);
        Task DeleteAsync(int id);
    }
}
