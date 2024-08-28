using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Models
{
    public class RealEstateFeatureValue
    {
        public int Id { get; set; }

        [Required]
        public int RealEstateId { get; set; }
        public RealEstate RealEstate { get; set; }

        [Required]
        public int FeatureId { get; set; }
        public DynamicFeature Feature { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
