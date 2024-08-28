using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public interface IRealEstateTypeFeatureCategoryRepository
    {
        Task<IEnumerable<RealEstateTypeFeatureCategory>> GetAllAsync();
        Task<RealEstateTypeFeatureCategory> GetByIdAsync(int realEstateTypeId, int featureCategoryId);
        Task AddAsync(RealEstateTypeFeatureCategory realEstateTypeFeatureCategory);
        Task DeleteAsync(int realEstateTypeId, int featureCategoryId);
    }
}
