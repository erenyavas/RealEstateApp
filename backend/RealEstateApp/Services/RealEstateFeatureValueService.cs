using AutoMapper;
using RealEstateApp.DTOs;
using RealEstateApp.Models;
using RealEstateApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public class RealEstateFeatureValueService : IRealEstateFeatureValueService
    {
        private readonly IRealEstateFeatureValueRepository _featureValueRepository;
        private readonly IMapper _mapper;

        public RealEstateFeatureValueService(IRealEstateFeatureValueRepository featureValueRepository, IMapper mapper)
        {
            _featureValueRepository = featureValueRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RealEstateFeatureValueDto>> GetAllAsync()
        {
            var featureValues = await _featureValueRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RealEstateFeatureValueDto>>(featureValues);
        }

        public async Task<RealEstateFeatureValueDto> GetByIdAsync(int id)
        {
            var featureValue = await _featureValueRepository.GetByIdAsync(id);
            return _mapper.Map<RealEstateFeatureValueDto>(featureValue);
        }

        public async Task AddAsync(RealEstateFeatureValueDto featureValueDto)
        {
            var featureValue = _mapper.Map<RealEstateFeatureValue>(featureValueDto);
            await _featureValueRepository.AddAsync(featureValue);
        }

        public async Task AddBulkAsync(RealEstateFeatureValueBulkDto bulkDto)
        {

            foreach (var featureId in bulkDto.FeatureIds)
            {
                var featureValue = new RealEstateFeatureValue
                {
                    RealEstateId = bulkDto.RealEstateId,
                    FeatureId = featureId,
                    Value = bulkDto.Values[bulkDto.FeatureIds.IndexOf(featureId)]
                };
                await _featureValueRepository.AddAsync(featureValue);
            }

        }


        public async Task UpdateAsync(RealEstateFeatureValueDto featureValueDto)
        {
            var featureValue = _mapper.Map<RealEstateFeatureValue>(featureValueDto);
            await _featureValueRepository.UpdateAsync(featureValue);
        }

        public async Task DeleteAsync(int id)
        {
            var featureValue = await _featureValueRepository.GetByIdAsync(id);
            if (featureValue != null)
            {
                await _featureValueRepository.DeleteAsync(featureValue);
            }
        }
    }
}
