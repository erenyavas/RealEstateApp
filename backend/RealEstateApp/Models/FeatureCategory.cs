using AutoMapper.Features;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RealEstateApp.Models
{
    public class FeatureCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Feature> Features { get; set; }
        [JsonIgnore]
        public ICollection<RealEstateTypeFeatureCategory> RealEstateTypeFeatureCategories { get; set; }
    }
}
