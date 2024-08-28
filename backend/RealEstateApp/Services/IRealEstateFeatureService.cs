using RealEstateApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public interface IRealEstateFeatureService
    {
        Task<IEnumerable<RealEstateFeatureDto>> GetAllAsync();
        Task<RealEstateFeatureDto> GetByIdAsync(int realEstateId, int featureId);
        Task AddAsync(RealEstateFeatureDto realEstateFeatureDto);
        Task AddBulkAsync(RealEstateFeatureBulkDto realEstateFeatureBulkDto);
        Task DeleteAsync(int realEstateId, int featureId);
    }
}
