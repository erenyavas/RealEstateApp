using System.Collections.Generic;

namespace RealEstateApp.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public ICollection<RealEstate> RealEstates { get; set; }
    }
}
