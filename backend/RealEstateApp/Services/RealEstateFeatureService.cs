using AutoMapper;
using RealEstateApp.DTOs;
using RealEstateApp.Models;
using RealEstateApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public class RealEstateFeatureService : IRealEstateFeatureService
    {
        private readonly IRealEstateFeatureRepository _realEstateFeatureRepository;
        private readonly IMapper _mapper;

        public RealEstateFeatureService(IRealEstateFeatureRepository realEstateFeatureRepository, IMapper mapper)
        {
            _realEstateFeatureRepository = realEstateFeatureRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RealEstateFeatureDto>> GetAllAsync()
        {
            var features = await _realEstateFeatureRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RealEstateFeatureDto>>(features);
        }

        public async Task<RealEstateFeatureDto> GetByIdAsync(int realEstateId, int featureId)
        {
            var feature = await _realEstateFeatureRepository.GetByIdAsync(realEstateId, featureId);
            return _mapper.Map<RealEstateFeatureDto>(feature);
        }

        public async Task AddAsync(RealEstateFeatureDto realEstateFeatureDto)
        {
            var feature = _mapper.Map<RealEstateFeature>(realEstateFeatureDto);
            await _realEstateFeatureRepository.AddAsync(feature);
        }

        public async Task AddBulkAsync(RealEstateFeatureBulkDto realEstateFeatureBulkDto)
        {
            foreach (var featureId in realEstateFeatureBulkDto.FeatureIds)
            {
                var realEstateFeature = new RealEstateFeature
                {
                    RealEstateId = realEstateFeatureBulkDto.RealEstateId,
                    FeatureId = featureId
                };
                await _realEstateFeatureRepository.AddAsync(realEstateFeature);
            }
        }

        public async Task DeleteAsync(int realEstateId, int featureId)
        {
            await _realEstateFeatureRepository.DeleteAsync(realEstateId, featureId);
        }
    }
}
