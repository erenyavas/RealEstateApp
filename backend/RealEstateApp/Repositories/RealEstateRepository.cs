using Microsoft.EntityFrameworkCore;
using RealEstateApp.Data;
using RealEstateApp.DTOs;
using RealEstateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateApp.Repositories
{
    public class RealEstateRepository : IRealEstateRepository
    {
        private readonly ApplicationDbContext _context;

        public RealEstateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RealEstate>> GetAllAsync()
        {
            return await _context.RealEstates
                .Include(re => re.RealEstateType)
                .Include(re => re.RealEstateStatus)
                .Include(re => re.Currency)
                .Include(re => re.Owner)
                .Include(re => re.City)
                .Include(re => re.District)
                .Include(re => re.Neighborhood)
                .Include(re => re.RealEstateFeatureValues)
                    .ThenInclude(fv => fv.Feature)
                .Include(re => re.RealEstateFeatures)
                .ToListAsync();
        }

        


        public async Task<RealEstate> GetByIdAsync(int id)
        {
            return await _context.RealEstates
                .Include(re => re.RealEstateType)
                .Include(re => re.RealEstateStatus)
                .Include(re => re.Currency)
                .Include(re => re.Owner)
                .Include(re => re.City)
                .Include(re => re.District)
                .Include(re => re.Neighborhood)
                .Include(re => re.RealEstateFeatureValues)
                    .ThenInclude(fv => fv.Feature)
                .Include(re => re.RealEstateFeatures)
                .FirstOrDefaultAsync(re => re.Id == id);
        }


        public async Task<(IEnumerable<RealEstate> RealEstates, int TotalCount)> GetPagedRealEstatesAsync(RealEstateQueryParameters queryParameters)
        {
            var query = _context.RealEstates
                .Include(re => re.RealEstateType)
                .Include(re => re.RealEstateStatus)
                .Include(re => re.Currency)
                .Include(re => re.City)
                .Include(re => re.District)
                .Include(re => re.Neighborhood)
                .Include(re => re.Owner)
                .Include(re => re.RealEstateFeatureValues)
                    .ThenInclude(fv => fv.Feature)
                .Include(re => re.RealEstateFeatures)
                    .ThenInclude(f => f.Feature)
                .AsQueryable();

            if (!string.IsNullOrEmpty(queryParameters.OwnerId))
            {
                query = query.Where(re => re.OwnerId == queryParameters.OwnerId);
            }

            // Search Filters
            if (!string.IsNullOrEmpty(queryParameters.Title))
            {
                query = query.Where(re => re.Title.Contains(queryParameters.Title));
            }

            if (!string.IsNullOrEmpty(queryParameters.Description))
            {
                query = query.Where(re => re.Description.Contains(queryParameters.Description));
            }

            if (!string.IsNullOrEmpty(queryParameters.OwnerFirstName))
            {
                query = query.Where(re => re.Owner.FirstName.Contains(queryParameters.OwnerFirstName));
            }

            if (!string.IsNullOrEmpty(queryParameters.OwnerLastName))
            {
                query = query.Where(re => re.Owner.LastName.Contains(queryParameters.OwnerLastName));
            }

            // Multiple Filters
            if (queryParameters.RealEstateTypeIds != null && queryParameters.RealEstateTypeIds.Any())
            {
                query = query.Where(re => queryParameters.RealEstateTypeIds.Contains(re.RealEstateTypeId));
            }

            if (queryParameters.RealEstateStatusIds != null && queryParameters.RealEstateStatusIds.Any())
            {
                query = query.Where(re => queryParameters.RealEstateStatusIds.Contains(re.RealEstateStatusId));
            }

            if (queryParameters.CurrencyIds != null && queryParameters.CurrencyIds.Any())
            {
                query = query.Where(re => queryParameters.CurrencyIds.Contains(re.CurrencyId));
            }

            if (queryParameters.CityIds != null && queryParameters.CityIds.Any())
            {
                query = query.Where(re => queryParameters.CityIds.Contains(re.CityId));
            }

            if (queryParameters.DistrictIds != null && queryParameters.DistrictIds.Any())
            {
                query = query.Where(re => queryParameters.DistrictIds.Contains(re.DistrictId));
            }

            if (queryParameters.NeighborhoodIds != null && queryParameters.NeighborhoodIds.Any())
            {
                query = query.Where(re => queryParameters.NeighborhoodIds.Contains(re.NeighborhoodId));
            }

            // Range Filters
            if (queryParameters.MinPrice.HasValue)
            {
                query = query.Where(re => re.Price >= queryParameters.MinPrice.Value);
            }

            if (queryParameters.MaxPrice.HasValue)
            {
                query = query.Where(re => re.Price <= queryParameters.MaxPrice.Value);
            }

            if (queryParameters.MinStartDate.HasValue)
            {
                query = query.Where(re => re.StartDate >= queryParameters.MinStartDate.Value);
            }

            if (queryParameters.MaxStartDate.HasValue)
            {
                query = query.Where(re => re.StartDate <= queryParameters.MaxStartDate.Value);
            }

            if (queryParameters.MinEndDate.HasValue)
            {
                query = query.Where(re => re.EndDate >= queryParameters.MinEndDate.Value);
            }

            if (queryParameters.MaxEndDate.HasValue)
            {
                query = query.Where(re => re.EndDate <= queryParameters.MaxEndDate.Value);
            }

            // Sorting
            if (!string.IsNullOrEmpty(queryParameters.SortBy))
            {
                query = queryParameters.SortBy switch
                {
                    "price" => queryParameters.SortOrder == "ascend" ? query.OrderBy(re => re.Price) : query.OrderByDescending(re => re.Price),
                    "startDate" => queryParameters.SortOrder == "ascend" ? query.OrderBy(re => re.StartDate) : query.OrderByDescending(re => re.StartDate),
                    "endDate" => queryParameters.SortOrder == "ascend" ? query.OrderBy(re => re.EndDate) : query.OrderByDescending(re => re.EndDate),
                    _ => query.OrderBy(re => re.Id)
                };
            }
            else
            {
                query = query.OrderBy(re => re.Id); 
            }

            
            var totalCount = await query.CountAsync();

            // Paging
            var realEstates = await query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize)
                .ToListAsync();

            return (RealEstates: realEstates, TotalCount: totalCount);
        }


        public async Task<int> CountAsync()
        {
            return await _context.RealEstates.CountAsync();
        }


        public async Task AddAsync(RealEstate realEstate)
        {
            await _context.RealEstates.AddAsync(realEstate);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RealEstate realEstate)
        {
            _context.RealEstates.Update(realEstate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RealEstate realEstate)
        {
            _context.RealEstates.Remove(realEstate);
            await _context.SaveChangesAsync();
        }
    }
}
