using RealEstateApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateApp.Helpers;

namespace RealEstateApp.Services
{
    public interface IRealEstateService
    {
        Task<IEnumerable<RealEstateDto>> GetAllAsync();
        Task<RealEstateDto> GetByIdAsync(int id);
        Task<PagedResult<RealEstateDto>> GetPagedRealEstatesAsync(RealEstateQueryParameters queryParameters);
        Task AddAsync(RealEstateDto realEstateDto);
        Task UpdateAsync(RealEstateEditDto realEstateEditDto);
        Task DeleteAsync(int id);
    }
}
