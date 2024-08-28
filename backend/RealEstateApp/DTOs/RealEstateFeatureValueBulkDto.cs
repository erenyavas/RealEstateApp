using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.DTOs
{
    public class RealEstateFeatureValueBulkDto
    {
        [Required]
        public int RealEstateId { get; set; }

        [Required]
        public List<int> FeatureIds { get; set; }

        [Required]
        public List<string> Values { get; set; }
    }
}
