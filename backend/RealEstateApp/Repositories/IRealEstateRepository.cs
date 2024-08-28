using RealEstateApp.DTOs;
using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public interface IRealEstateRepository
    {
        Task<IEnumerable<RealEstate>> GetAllAsync();
        Task<RealEstate> GetByIdAsync(int id);
        Task<(IEnumerable<RealEstate> RealEstates, int TotalCount)> GetPagedRealEstatesAsync(RealEstateQueryParameters queryParameters);
        Task AddAsync(RealEstate realEstate);
        Task UpdateAsync(RealEstate realEstate);
        Task DeleteAsync(RealEstate realEstate);
    }
}
