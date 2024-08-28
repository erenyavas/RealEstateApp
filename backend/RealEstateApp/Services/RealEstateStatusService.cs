using RealEstateApp.Models;
using RealEstateApp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public class RealEstateStatusService : IRealEstateStatusService
    {
        private readonly IRealEstateStatusRepository _realEstateStatusRepository;

        public RealEstateStatusService(IRealEstateStatusRepository realEstateStatusRepository)
        {
            _realEstateStatusRepository = realEstateStatusRepository;
        }

        public async Task<IEnumerable<RealEstateStatus>> GetAllAsync()
        {
            return await _realEstateStatusRepository.GetAllAsync();
        }

        public async Task<RealEstateStatus> GetByIdAsync(int id)
        {
            return await _realEstateStatusRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(RealEstateStatus realEstateStatus)
        {
            await _realEstateStatusRepository.AddAsync(realEstateStatus);
        }

        public async Task UpdateAsync(RealEstateStatus realEstateStatus)
        {
            await _realEstateStatusRepository.UpdateAsync(realEstateStatus);
        }

        public async Task DeleteAsync(RealEstateStatus realEstateStatus)
        {
            await _realEstateStatusRepository.DeleteAsync(realEstateStatus);
        }
    }
}
