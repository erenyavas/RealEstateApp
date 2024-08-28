using RealEstateApp.Models;
using RealEstateApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public class RealEstateTypeService : IRealEstateTypeService
    {
        private readonly IRealEstateTypeRepository _realEstateTypeRepository;

        public RealEstateTypeService(IRealEstateTypeRepository realEstateTypeRepository)
        {
            _realEstateTypeRepository = realEstateTypeRepository;
        }

        public async Task<IEnumerable<RealEstateType>> GetAllAsync()
        {
            return await _realEstateTypeRepository.GetAllAsync();
        }

        public async Task<RealEstateType> GetByIdAsync(int id)
        {
            return await _realEstateTypeRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(RealEstateType realEstateType)
        {
            await _realEstateTypeRepository.AddAsync(realEstateType);
        }

        public async Task UpdateAsync(RealEstateType realEstateType)
        {
            await _realEstateTypeRepository.UpdateAsync(realEstateType);
        }

        public async Task DeleteAsync(RealEstateType realEstateType)
        {
            await _realEstateTypeRepository.DeleteAsync(realEstateType);
        }
    }
}
