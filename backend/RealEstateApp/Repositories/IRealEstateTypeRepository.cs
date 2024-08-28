using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public interface IRealEstateTypeRepository
    {
        Task<IEnumerable<RealEstateType>> GetAllAsync();
        Task<RealEstateType> GetByIdAsync(int id);
        Task AddAsync(RealEstateType realEstateType);
        Task UpdateAsync(RealEstateType realEstateType);
        Task DeleteAsync(RealEstateType realEstateType);
    }
}
