using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.DTOs
{
    public class RealEstateFeatureValueDto
    {
        public int Id { get; set; }

        [Required]
        public int RealEstateId { get; set; }

        [Required]
        public int FeatureId { get; set; }
        public DynamicFeatureDto Feature { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
