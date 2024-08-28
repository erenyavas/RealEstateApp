using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Models
{
    public class District
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int CityId { get; set; }
        public City City { get; set; }

        public ICollection<Neighborhood> Neighborhoods { get; set; }
    }
}
