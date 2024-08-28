namespace RealEstateApp.DTOs
{
    public class RealEstateQueryParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Search and Filter Parameters
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<int>? RealEstateTypeIds { get; set; }
        public List<int>? RealEstateStatusIds { get; set; }
        public List<int>? CurrencyIds { get; set; }
        public List<int>? CityIds { get; set; }
        public List<int>? DistrictIds { get; set; }
        public List<int>? NeighborhoodIds { get; set; }
        public string? OwnerFirstName { get; set; }
        public string? OwnerLastName { get; set; }

        public string? OwnerId { get; set; }

        // Range Filters
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? MinStartDate { get; set; }
        public DateTime? MaxStartDate { get; set; }
        public DateTime? MinEndDate { get; set; }
        public DateTime? MaxEndDate { get; set; }

        // Sorting
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; } = "ascend"; // "asc" or "desc"
    }

}
