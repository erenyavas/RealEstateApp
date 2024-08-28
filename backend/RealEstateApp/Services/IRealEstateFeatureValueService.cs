using RealEstateApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public interface IRealEstateFeatureValueService
    {
        Task<IEnumerable<RealEstateFeatureValueDto>> GetAllAsync();
        Task<RealEstateFeatureValueDto> GetByIdAsync(int id);
        Task AddAsync(RealEstateFeatureValueDto featureValueDto);
        Task AddBulkAsync(RealEstateFeatureValueBulkDto bulkDto);
        Task UpdateAsync(RealEstateFeatureValueDto featureValueDto);
        Task DeleteAsync(int id);
    }
}
