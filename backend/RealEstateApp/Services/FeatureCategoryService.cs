using AutoMapper;
using RealEstateApp.DTOs;
using RealEstateApp.Models;
using RealEstateApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public class FeatureCategoryService : IFeatureCategoryService
    {
        private readonly IFeatureCategoryRepository _featureCategoryRepository;
        private readonly IMapper _mapper;

        public FeatureCategoryService(IFeatureCategoryRepository featureCategoryRepository, IMapper mapper)
        {
            _featureCategoryRepository = featureCategoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FeatureCategoryDto>> GetAllAsync()
        {
            var categories = await _featureCategoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FeatureCategoryDto>>(categories);
        }

        public async Task<FeatureCategoryDto> GetByIdAsync(int id)
        {
            var category = await _featureCategoryRepository.GetByIdAsync(id);
            return _mapper.Map<FeatureCategoryDto>(category);
        }

        public async Task AddAsync(FeatureCategoryDto featureCategoryDto)
        {
            var category = _mapper.Map<FeatureCategory>(featureCategoryDto);
            await _featureCategoryRepository.AddAsync(category);
        }

        public async Task UpdateAsync(FeatureCategoryDto featureCategoryDto)
        {
            var category = _mapper.Map<FeatureCategory>(featureCategoryDto);
            await _featureCategoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _featureCategoryRepository.DeleteAsync(id);
        }
    }
}
