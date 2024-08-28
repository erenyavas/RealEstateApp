using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public interface IRealEstateStatusRepository
    {
        Task<IEnumerable<RealEstateStatus>> GetAllAsync();
        Task<RealEstateStatus> GetByIdAsync(int id);
        Task AddAsync(RealEstateStatus realEstateStatus);
        Task UpdateAsync(RealEstateStatus realEstateStatus);
        Task DeleteAsync(RealEstateStatus realEstateStatus);
    }
}
