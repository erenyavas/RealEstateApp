namespace RealEstateApp.Models
{
    public class RealEstateTypeFeatureCategory
    {
        public int RealEstateTypeId { get; set; }
        public RealEstateType RealEstateType { get; set; }

        public int FeatureCategoryId { get; set; }
        public FeatureCategory FeatureCategory { get; set; }
    }
}
