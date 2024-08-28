using AutoMapper;
using RealEstateApp.DTOs;
using RealEstateApp.Models;
using RealEstateApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public class RealEstateTypeFeatureService : IRealEstateTypeFeatureService
    {
        private readonly IRealEstateTypeFeatureRepository _realEstateTypeFeatureRepository;
        private readonly IMapper _mapper;

        public RealEstateTypeFeatureService(IRealEstateTypeFeatureRepository realEstateTypeFeatureRepository, IMapper mapper)
        {
            _realEstateTypeFeatureRepository = realEstateTypeFeatureRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RealEstateTypeFeatureDto>> GetAllAsync()
        {
            var realEstateTypeFeatures = await _realEstateTypeFeatureRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RealEstateTypeFeatureDto>>(realEstateTypeFeatures);
        }

        public async Task<RealEstateTypeFeatureDto> GetByIdAsync(int realEstateTypeId, int featureId)
        {
            var realEstateTypeFeature = await _realEstateTypeFeatureRepository.GetByIdAsync(realEstateTypeId, featureId);
            return _mapper.Map<RealEstateTypeFeatureDto>(realEstateTypeFeature);
        }

        public async Task AddAsync(RealEstateTypeFeatureDto realEstateTypeFeatureDto)
        {
            foreach (var featureId in realEstateTypeFeatureDto.FeatureIds)
            {
                var realEstateTypeFeature = new RealEstateTypeFeature
                {
                    RealEstateTypeId = realEstateTypeFeatureDto.RealEstateTypeId,
                    FeatureId = featureId
                };

                await _realEstateTypeFeatureRepository.AddAsync(realEstateTypeFeature);
            }
        }

        public async Task DeleteAsync(int realEstateTypeId, int featureId)
        {
            var realEstateTypeFeature = await _realEstateTypeFeatureRepository.GetByIdAsync(realEstateTypeId, featureId);
            if (realEstateTypeFeature != null)
            {
                await _realEstateTypeFeatureRepository.DeleteAsync(realEstateTypeFeature);
            }
        }
    }
}
