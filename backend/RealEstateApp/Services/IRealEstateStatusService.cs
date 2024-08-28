using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public interface IRealEstateStatusService
    {
        Task<IEnumerable<RealEstateStatus>> GetAllAsync();
        Task<RealEstateStatus> GetByIdAsync(int id);
        Task AddAsync(RealEstateStatus realEstateStatus);
        Task UpdateAsync(RealEstateStatus realEstateStatus);
        Task DeleteAsync(RealEstateStatus realEstateStatus);
    }
}
