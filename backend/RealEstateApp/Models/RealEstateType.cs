using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Models
{
    public class RealEstateType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<RealEstate> RealEstates { get; set; }

        public ICollection<RealEstateTypeFeatureCategory> RealEstateTypeFeatureCategories { get; set; }
        public ICollection<RealEstateTypeFeature> RealEstateTypeFeatures { get; set; }
    }
}
