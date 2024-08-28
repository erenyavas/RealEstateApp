using RealEstateApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public interface IDynamicFeatureService
    {
        Task<IEnumerable<DynamicFeatureDto>> GetAllAsync();
        Task<DynamicFeatureDto> GetByIdAsync(int id);
        Task AddAsync(DynamicFeatureDto dynamicFeatureDto);
        Task UpdateAsync(DynamicFeatureDto dynamicFeatureDto);
        Task DeleteAsync(int id);
    }
}
