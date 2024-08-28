using Newtonsoft.Json;

namespace RealEstateApp.Models
{
    public class RealEstateTypeFeature
    {
        public int RealEstateTypeId { get; set; }

        [JsonIgnore]
        public RealEstateType RealEstateType { get; set; }

        public int FeatureId { get; set; }
        public DynamicFeature Feature { get; set; }
    }
}
