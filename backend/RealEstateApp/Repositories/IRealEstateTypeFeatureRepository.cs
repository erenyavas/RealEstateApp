using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public interface IRealEstateTypeFeatureRepository
    {
        Task<IEnumerable<RealEstateTypeFeature>> GetAllAsync();
        Task<RealEstateTypeFeature> GetByIdAsync(int realEstateTypeId, int featureId);
        Task AddAsync(RealEstateTypeFeature realEstateTypeFeature);
        Task DeleteAsync(RealEstateTypeFeature realEstateTypeFeature);
    }
}
