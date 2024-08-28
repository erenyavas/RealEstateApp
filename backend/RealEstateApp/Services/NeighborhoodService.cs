using AutoMapper;
using RealEstateApp.DTOs;
using RealEstateApp.Models;
using RealEstateApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public class NeighborhoodService : INeighborhoodService
    {
        private readonly INeighborhoodRepository _neighborhoodRepository;
        private readonly IMapper _mapper;

        public NeighborhoodService(INeighborhoodRepository neighborhoodRepository, IMapper mapper)
        {
            _neighborhoodRepository = neighborhoodRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NeighborhoodDto>> GetAllAsync()
        {
            var neighborhoods = await _neighborhoodRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<NeighborhoodDto>>(neighborhoods);
        }

        public async Task<NeighborhoodDto> GetByIdAsync(int id)
        {
            var neighborhood = await _neighborhoodRepository.GetByIdAsync(id);
            return _mapper.Map<NeighborhoodDto>(neighborhood);
        }

        public async Task<IEnumerable<NeighborhoodDto>> GetNeighborhoodsByDistrictIdAsync(int districtId)
        {
            var neighborhoods = await _neighborhoodRepository.GetNeighborhoodsByDistrictIdAsync(districtId);
            return _mapper.Map<IEnumerable<NeighborhoodDto>>(neighborhoods);
        }


        public async Task AddAsync(NeighborhoodDto neighborhoodDto)
        {
            var neighborhood = _mapper.Map<Neighborhood>(neighborhoodDto);
            await _neighborhoodRepository.AddAsync(neighborhood);
        }

        public async Task UpdateAsync(NeighborhoodDto neighborhoodDto)
        {
            var neighborhood = _mapper.Map<Neighborhood>(neighborhoodDto);
            await _neighborhoodRepository.UpdateAsync(neighborhood);
        }

        public async Task DeleteAsync(int id)
        {
            var neighborhood = await _neighborhoodRepository.GetByIdAsync(id);
            await _neighborhoodRepository.DeleteAsync(neighborhood);
        }
    }
}
