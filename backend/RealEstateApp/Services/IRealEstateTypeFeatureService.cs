using RealEstateApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public interface IRealEstateTypeFeatureService
    {
        Task<IEnumerable<RealEstateTypeFeatureDto>> GetAllAsync();
        Task<RealEstateTypeFeatureDto> GetByIdAsync(int realEstateTypeId, int featureId);
        Task AddAsync(RealEstateTypeFeatureDto realEstateTypeFeatureDto);
        Task DeleteAsync(int realEstateTypeId, int featureId);
    }
}
