using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public interface IDynamicFeatureRepository
    {
        Task<IEnumerable<DynamicFeature>> GetAllAsync();
        Task<DynamicFeature> GetByIdAsync(int id);
        Task AddAsync(DynamicFeature dynamicFeature);
        Task UpdateAsync(DynamicFeature dynamicFeature);
        Task DeleteAsync(DynamicFeature dynamicFeature);
    }
}
