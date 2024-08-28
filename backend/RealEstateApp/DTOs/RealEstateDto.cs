using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.DTOs
{
  public class RealEstateDto
  {
    public int Id { get; set; }


    public int? RealEstateTypeId { get; set; }
    public RealEstateTypeDto? RealEstateType { get; set; }


    public int RealEstateStatusId { get; set; }
    public RealEstateStatusDto? RealEstateStatus { get; set; }


    public DateTime? StartDate { get; set; }


    public DateTime? EndDate { get; set; }


    public decimal Price { get; set; }


    public int CurrencyId { get; set; }
    public CurrencyDto? Currency { get; set; }


    public string? OwnerId { get; set; }
    public ProfileDto? Owner { get; set; }


    public string Title { get; set; }


    public string Description { get; set; }


    public decimal SquareMeters { get; set; }

    public List<string>? Photos { get; set; } = new List<string>();

    public decimal X { get; set; }
    public decimal Y { get; set; }

    [Required]
    public int CityId { get; set; }
    public CityDto? City { get; set; }

    [Required]
    public int DistrictId { get; set; }
    public DistrictDto? District { get; set; }

    [Required]
    public int NeighborhoodId { get; set; }
    public NeighborhoodDto? Neighborhood { get; set; }

    public ICollection<RealEstateFeatureDto>? RealEstateFeatures { get; set; }
    public ICollection<RealEstateFeatureValueDto>? RealEstateFeatureValues { get; set; }
  }
}
