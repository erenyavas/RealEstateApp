using RealEstateApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public interface IDistrictService
    {
        Task<IEnumerable<DistrictDto>> GetAllAsync();
        Task<DistrictDto> GetByIdAsync(int id);
        Task<IEnumerable<DistrictDto>> GetDistrictsByCityIdAsync(int cityId);
        Task AddAsync(DistrictDto districtDto);
        Task UpdateAsync(DistrictDto districtDto);
        Task DeleteAsync(int id);
    }
}
