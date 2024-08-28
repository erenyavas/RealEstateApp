using AutoMapper;
using RealEstateApp.DTOs;
using RealEstateApp.Models;
using RealEstateApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;
        
        public CurrencyService(ICurrencyRepository currencyRepository, IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CurrencyDto>> GetAllAsync()
        {
            var currencies = await _currencyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CurrencyDto>>(currencies);
        }

        public async Task<CurrencyDto> GetByIdAsync(int id)
        {
            var currency = await _currencyRepository.GetByIdAsync(id);
            return _mapper.Map<CurrencyDto>(currency);
        }

        public async Task AddAsync(CurrencyDto currencyDto)
        {
            var currency = _mapper.Map<Currency>(currencyDto);
            await _currencyRepository.AddAsync(currency);
        }

        public async Task UpdateAsync(CurrencyDto currencyDto)
        {
            var currency = _mapper.Map<Currency>(currencyDto);
            await _currencyRepository.UpdateAsync(currency);
        }

        public async Task DeleteAsync(int id)
        {
            var currency = await _currencyRepository.GetByIdAsync(id);
            await _currencyRepository.DeleteAsync(currency);
        }
    }
}
