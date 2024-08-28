namespace RealEstateApp.Models
{
    public class RealEstateFeature
    {
        public int RealEstateId { get; set; }
        public RealEstate RealEstate { get; set; }

        public int FeatureId { get; set; }
        public Feature Feature { get; set; }
    }
}
