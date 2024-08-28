using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public interface IRealEstateFeatureValueRepository
    {
        Task<IEnumerable<RealEstateFeatureValue>> GetAllAsync();
        Task<RealEstateFeatureValue> GetByIdAsync(int id);
        Task AddAsync(RealEstateFeatureValue featureValue);
        Task UpdateAsync(RealEstateFeatureValue featureValue);
        Task DeleteAsync(RealEstateFeatureValue featureValue);
    }
}
