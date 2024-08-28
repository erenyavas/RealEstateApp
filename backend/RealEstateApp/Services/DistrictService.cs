using AutoMapper;
using RealEstateApp.DTOs;
using RealEstateApp.Models;
using RealEstateApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly IMapper _mapper;

        public DistrictService(IDistrictRepository districtRepository, IMapper mapper)
        {
            _districtRepository = districtRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DistrictDto>> GetAllAsync()
        {
            var districts = await _districtRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DistrictDto>>(districts);
        }

        public async Task<DistrictDto> GetByIdAsync(int id)
        {
            var district = await _districtRepository.GetByIdAsync(id);
            return _mapper.Map<DistrictDto>(district);
        }

        public async Task AddAsync(DistrictDto districtDto)
        {
            var district = _mapper.Map<District>(districtDto);
            await _districtRepository.AddAsync(district);
        }

        public async Task<IEnumerable<DistrictDto>> GetDistrictsByCityIdAsync(int cityId)
        {
            var districts = await _districtRepository.GetDistrictsByCityIdAsync(cityId);
            return _mapper.Map<IEnumerable<DistrictDto>>(districts);
        }


        public async Task UpdateAsync(DistrictDto districtDto)
        {
            var district = _mapper.Map<District>(districtDto);
            await _districtRepository.UpdateAsync(district);
        }

        public async Task DeleteAsync(int id)
        {
            var district = await _districtRepository.GetByIdAsync(id);
            await _districtRepository.DeleteAsync(district);
        }
    }
}
