using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Models
{
    public class Feature
    {
        public int Id { get; set; }

        [Required]
        public int FeatureCategoryId { get; set; }
        public FeatureCategory FeatureCategory { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<RealEstateFeature> RealEstateFeatures { get; set; }
    }
}
