namespace RealEstateApp.DTOs
{
    public class FeatureCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FeatureDto>? Features { get; set; }
    }
}
