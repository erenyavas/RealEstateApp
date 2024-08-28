using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public interface IRealEstateFeatureRepository
    {
        Task<IEnumerable<RealEstateFeature>> GetAllAsync();
        Task<RealEstateFeature> GetByIdAsync(int realEstateId, int featureId);
        Task AddAsync(RealEstateFeature realEstateFeature);
        Task DeleteAsync(int realEstateId, int featureId);
    }
}
