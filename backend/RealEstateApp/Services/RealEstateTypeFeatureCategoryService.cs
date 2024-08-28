using AutoMapper;
using RealEstateApp.DTOs;
using RealEstateApp.Models;
using RealEstateApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public class RealEstateTypeFeatureCategoryService : IRealEstateTypeFeatureCategoryService
    {
        private readonly IRealEstateTypeFeatureCategoryRepository _realEstateTypeFeatureCategoryRepository;
        private readonly IMapper _mapper;

        public RealEstateTypeFeatureCategoryService(IRealEstateTypeFeatureCategoryRepository realEstateTypeFeatureCategoryRepository, IMapper mapper)
        {
            _realEstateTypeFeatureCategoryRepository = realEstateTypeFeatureCategoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RealEstateTypeFeatureCategoryDto>> GetAllAsync()
        {
            var categories = await _realEstateTypeFeatureCategoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RealEstateTypeFeatureCategoryDto>>(categories);
        }

        public async Task<RealEstateTypeFeatureCategoryDto> GetByIdAsync(int realEstateTypeId, int featureCategoryId)
        {
            var category = await _realEstateTypeFeatureCategoryRepository.GetByIdAsync(realEstateTypeId, featureCategoryId);
            return _mapper.Map<RealEstateTypeFeatureCategoryDto>(category);
        }

        public async Task AddAsync(RealEstateTypeFeatureCategoryDto realEstateTypeFeatureCategoryDto)
        {
            var category = _mapper.Map<RealEstateTypeFeatureCategory>(realEstateTypeFeatureCategoryDto);
            await _realEstateTypeFeatureCategoryRepository.AddAsync(category);
        }

        public async Task AddCategoriesAsync(int realEstateTypeId, List<int> categoryIds)
        {
            foreach (var categoryId in categoryIds)
            {
                var category = new RealEstateTypeFeatureCategory
                {
                    RealEstateTypeId = realEstateTypeId,
                    FeatureCategoryId = categoryId
                };
                await _realEstateTypeFeatureCategoryRepository.AddAsync(category);
            }
        }


        public async Task DeleteAsync(int realEstateTypeId, int featureCategoryId)
        {
            await _realEstateTypeFeatureCategoryRepository.DeleteAsync(realEstateTypeId, featureCategoryId);
        }
    }
}
