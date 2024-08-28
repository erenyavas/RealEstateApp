namespace RealEstateApp.DTOs
{
    public class RealEstateFeatureBulkDto
    {
        public int RealEstateId { get; set; }
        public List<int> FeatureIds { get; set; }
    }
}
