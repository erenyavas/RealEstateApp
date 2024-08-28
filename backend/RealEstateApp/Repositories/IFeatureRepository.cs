using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public interface IFeatureRepository
    {
        Task<IEnumerable<Feature>> GetAllAsync();
        Task<Feature> GetByIdAsync(int id);
        Task<IEnumerable<Feature>> GetByCategoryIdAsync(int categoryId);
        Task AddAsync(Feature feature);
        Task UpdateAsync(Feature feature);
        Task DeleteAsync(int id);
    }
}
