using RealEstateApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public interface IRealEstateTypeFeatureCategoryService
    {
        Task<IEnumerable<RealEstateTypeFeatureCategoryDto>> GetAllAsync();
        Task<RealEstateTypeFeatureCategoryDto> GetByIdAsync(int realEstateTypeId, int featureCategoryId);
        Task AddAsync(RealEstateTypeFeatureCategoryDto realEstateTypeFeatureCategoryDto);
        Task AddCategoriesAsync(int realEstateTypeId, List<int> categoryIds);
        Task DeleteAsync(int realEstateTypeId, int featureCategoryId);
    }
}
