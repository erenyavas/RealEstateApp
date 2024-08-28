using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAllAsync();
        Task<City> GetByIdAsync(int id);
        Task AddAsync(City city);
        Task UpdateAsync(City city);
        Task DeleteAsync(City city);
    }
}
