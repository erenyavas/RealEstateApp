using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<District> Districts { get; set; }
    }
}
