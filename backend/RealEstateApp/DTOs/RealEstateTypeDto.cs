namespace RealEstateApp.DTOs
{
    public class RealEstateTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RealEstateTypeFeatureDto>? RealEstateTypeFeatures { get; set; }
        public ICollection<FeatureCategoryDto>? RealEstateTypeFeatureCategories { get; set; }
    }
}
