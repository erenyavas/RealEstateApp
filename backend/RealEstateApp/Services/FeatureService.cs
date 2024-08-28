using AutoMapper;
using RealEstateApp.DTOs;
using RealEstateApp.Models;
using RealEstateApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;
        private readonly IMapper _mapper;

        public FeatureService(IFeatureRepository featureRepository, IMapper mapper)
        {
            _featureRepository = featureRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FeatureDto>> GetAllAsync()
        {
            var features = await _featureRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FeatureDto>>(features);
        }

        public async Task<FeatureDto> GetByIdAsync(int id)
        {
            var feature = await _featureRepository.GetByIdAsync(id);
            return _mapper.Map<FeatureDto>(feature);
        }

        public async Task<IEnumerable<Feature>> GetByCategoryIdAsync(int categoryId)
        {
            return await _featureRepository.GetByCategoryIdAsync(categoryId);
        }


        public async Task AddAsync(FeatureDto featureDto)
        {
            var feature = _mapper.Map<Feature>(featureDto);
            await _featureRepository.AddAsync(feature);
        }

        public async Task UpdateAsync(FeatureDto featureDto)
        {
            var feature = _mapper.Map<Feature>(featureDto);
            await _featureRepository.UpdateAsync(feature);
        }

        public async Task DeleteAsync(int id)
        {
            await _featureRepository.DeleteAsync(id);
        }
    }
}
