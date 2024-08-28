using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace RealEstateApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<RealEstate> RealEstates { get; set; }
    }
}
