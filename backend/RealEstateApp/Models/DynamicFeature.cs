using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace RealEstateApp.Models
{
    public class DynamicFeature
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string DataType { get; set; } // int, string, selectbox

        public int? MinValue { get; set; } // int türündeki özellikler için

        public int? MaxValue { get; set; } // int türündeki özellikler için

        public List<string> Options { get; set; } // selectbox türündeki özellikler için seçenekler
        [JsonIgnore]
        public ICollection<RealEstateTypeFeature> RealEstateTypeFeatures { get; set; }
        public ICollection<RealEstateFeatureValue> RealEstateFeatureValues { get; set; }
    }
}
