using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Models
{
    public class Neighborhood
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int DistrictId { get; set; }
        public District District { get; set; }

        public ICollection<RealEstate> RealEstates { get; set; }
    }
}
