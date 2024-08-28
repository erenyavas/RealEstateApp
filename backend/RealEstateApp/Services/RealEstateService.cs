using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.DTOs;
using RealEstateApp.Helpers;
using RealEstateApp.Models;
using RealEstateApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public class RealEstateService : IRealEstateService
    {
        private readonly IRealEstateRepository _realEstateRepository;
        private readonly IMapper _mapper;

        public RealEstateService(IRealEstateRepository realEstateRepository, IMapper mapper)
        {
            _realEstateRepository = realEstateRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RealEstateDto>> GetAllAsync()
        {
            var realEstates = await _realEstateRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RealEstateDto>>(realEstates);
        }


        public async Task<RealEstateDto> GetByIdAsync(int id)
        {
            var realEstate = await _realEstateRepository.GetByIdAsync(id);
            return _mapper.Map<RealEstateDto>(realEstate);
        }

        public async Task<PagedResult<RealEstateDto>> GetPagedRealEstatesAsync(RealEstateQueryParameters queryParameters)
        {
            var (realEstates, totalCount) = await _realEstateRepository.GetPagedRealEstatesAsync(queryParameters);

            var pagedResult = new PagedResult<RealEstateDto>
            {
                Items = _mapper.Map<IEnumerable<RealEstateDto>>(realEstates),
                TotalRecords = totalCount,
                PageNumber = queryParameters.PageNumber,
                PageSize = queryParameters.PageSize
            };

            return pagedResult;
        }



        public async Task AddAsync(RealEstateDto realEstateDto)
        {
            var realEstate = _mapper.Map<RealEstate>(realEstateDto);
            realEstate.StartDate = DateTime.UtcNow;
            realEstate.EndDate = realEstate.StartDate.AddDays(30);
            await _realEstateRepository.AddAsync(realEstate);
            realEstateDto.Id = realEstate.Id;
        }

        public async Task UpdateAsync(RealEstateEditDto realEstateEditDto)
        {
            var realEstate = await _realEstateRepository.GetByIdAsync(realEstateEditDto.Id);
            if (realEstate == null) return;

            _mapper.Map(realEstateEditDto, realEstate);
            await _realEstateRepository.UpdateAsync(realEstate);
        }


        public async Task DeleteAsync(int id)
        {
            var realEstate = await _realEstateRepository.GetByIdAsync(id);
            await _realEstateRepository.DeleteAsync(realEstate);
        }
    }
}
