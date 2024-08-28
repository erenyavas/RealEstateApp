using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public interface IFeatureCategoryRepository
    {
        Task<IEnumerable<FeatureCategory>> GetAllAsync();
        Task<FeatureCategory> GetByIdAsync(int id);
        Task AddAsync(FeatureCategory featureCategory);
        Task UpdateAsync(FeatureCategory featureCategory);
        Task DeleteAsync(int id);
    }
}
