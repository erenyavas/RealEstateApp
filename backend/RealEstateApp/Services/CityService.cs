using AutoMapper;
using RealEstateApp.DTOs;
using RealEstateApp.Models;
using RealEstateApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityDto>> GetAllAsync()
        {
            var cities = await _cityRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CityDto>>(cities);
        }

        public async Task<CityDto> GetByIdAsync(int id)
        {
            var city = await _cityRepository.GetByIdAsync(id);
            return _mapper.Map<CityDto>(city);
        }

        public async Task AddAsync(CityDto cityDto)
        {
            var city = _mapper.Map<City>(cityDto);
            await _cityRepository.AddAsync(city);
            cityDto.Id = city.Id;
        }


        public async Task UpdateAsync(CityDto cityDto)
        {
            var city = _mapper.Map<City>(cityDto);
            await _cityRepository.UpdateAsync(city);
        }

        public async Task DeleteAsync(int id)
        {
            var city = await _cityRepository.GetByIdAsync(id);
            await _cityRepository.DeleteAsync(city);
        }
    }
}
