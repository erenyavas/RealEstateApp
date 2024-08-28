using System.Collections.Generic;

namespace RealEstateApp.Models
{
    public class RealEstateStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RealEstate> RealEstates { get; set; }
    }
}
