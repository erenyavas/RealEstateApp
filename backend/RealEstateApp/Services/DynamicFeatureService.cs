using AutoMapper;
using RealEstateApp.DTOs;
using RealEstateApp.Models;
using RealEstateApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public class DynamicFeatureService : IDynamicFeatureService
    {
        private readonly IDynamicFeatureRepository _dynamicFeatureRepository;
        private readonly IMapper _mapper;

        public DynamicFeatureService(IDynamicFeatureRepository dynamicFeatureRepository, IMapper mapper)
        {
            _dynamicFeatureRepository = dynamicFeatureRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DynamicFeatureDto>> GetAllAsync()
        {
            var dynamicFeatures = await _dynamicFeatureRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DynamicFeatureDto>>(dynamicFeatures);
        }

        public async Task<DynamicFeatureDto> GetByIdAsync(int id)
        {
            var dynamicFeature = await _dynamicFeatureRepository.GetByIdAsync(id);
            return _mapper.Map<DynamicFeatureDto>(dynamicFeature);
        }

        public async Task AddAsync(DynamicFeatureDto dynamicFeatureDto)
        {
            var dynamicFeature = _mapper.Map<DynamicFeature>(dynamicFeatureDto);
            await _dynamicFeatureRepository.AddAsync(dynamicFeature);
        }

        public async Task UpdateAsync(DynamicFeatureDto dynamicFeatureDto)
        {
            var dynamicFeature = _mapper.Map<DynamicFeature>(dynamicFeatureDto);
            await _dynamicFeatureRepository.UpdateAsync(dynamicFeature);
        }

        public async Task DeleteAsync(int id)
        {
            var dynamicFeature = await _dynamicFeatureRepository.GetByIdAsync(id);
            if (dynamicFeature != null)
            {
                await _dynamicFeatureRepository.DeleteAsync(dynamicFeature);
            }
        }
    }
}
