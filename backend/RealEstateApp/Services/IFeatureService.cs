using RealEstateApp.DTOs;
using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public interface IFeatureService
    {
        Task<IEnumerable<FeatureDto>> GetAllAsync();
        Task<FeatureDto> GetByIdAsync(int id);
        Task<IEnumerable<Feature>> GetByCategoryIdAsync(int categoryId);
        Task AddAsync(FeatureDto featureDto);
        Task UpdateAsync(FeatureDto featureDto);
        Task DeleteAsync(int id);
    }
}
