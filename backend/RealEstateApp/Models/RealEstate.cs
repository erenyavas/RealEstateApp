using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateApp.Models
{
    public class RealEstate
    {
        public int Id { get; set; }

        [Required]
        public int RealEstateTypeId { get; set; }
        public RealEstateType RealEstateType { get; set; }

        [Required]
        public int RealEstateStatusId { get; set; }
        public RealEstateStatus RealEstateStatus { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        [Required]
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SquareMeters { get; set; }

        public List<string> Photos { get; set; } = new List<string>();

        public decimal X { get; set; }
        public decimal Y { get; set; }

        [Required]
        public int CityId { get; set; }
        public City City { get; set; }

        [Required]
        public int DistrictId { get; set; }
        public District District { get; set; }

        [Required]
        public int NeighborhoodId { get; set; }
        public Neighborhood Neighborhood { get; set; }

       


        public ICollection<RealEstateFeatureValue> RealEstateFeatureValues { get; set; }
        public ICollection<RealEstateFeature> RealEstateFeatures { get; set; }
    }
}
