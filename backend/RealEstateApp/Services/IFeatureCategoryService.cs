using RealEstateApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public interface IFeatureCategoryService
    {
        Task<IEnumerable<FeatureCategoryDto>> GetAllAsync();
        Task<FeatureCategoryDto> GetByIdAsync(int id);
        Task AddAsync(FeatureCategoryDto featureCategoryDto);
        Task UpdateAsync(FeatureCategoryDto featureCategoryDto);
        Task DeleteAsync(int id);
    }
}
