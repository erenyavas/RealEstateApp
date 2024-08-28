using AutoMapper;
using RealEstateApp.DTOs;
using RealEstateApp.Models;

namespace RealEstateApp.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<District, DistrictDto>().ReverseMap();
            CreateMap<Neighborhood, NeighborhoodDto>().ReverseMap();
            CreateMap<Currency, CurrencyDto>().ReverseMap();
            CreateMap<RealEstateEditDto, RealEstate>();
            CreateMap<RealEstate, RealEstateDto>()
                .ForMember(dest => dest.RealEstateType, opt => opt.MapFrom(src => src.RealEstateType))
                .ForMember(dest => dest.RealEstateStatus, opt => opt.MapFrom(src => src.RealEstateStatus))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.District, opt => opt.MapFrom(src => src.District))
                .ForMember(dest => dest.Neighborhood, opt => opt.MapFrom(src => src.Neighborhood))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner))
                .ForMember(dest => dest.RealEstateFeatures, opt => opt.MapFrom(src => src.RealEstateFeatures))
                .ForMember(dest => dest.RealEstateFeatureValues, opt => opt.MapFrom(src => src.RealEstateFeatureValues))
                .ReverseMap();
            CreateMap<RealEstateStatus, RealEstateStatusDto>().ReverseMap();
            CreateMap<RealEstateType, RealEstateTypeDto>()
                .ForMember(dest => dest.RealEstateTypeFeatures, opt => opt.MapFrom(src => src.RealEstateTypeFeatures))
                .ForMember(dest => dest.RealEstateTypeFeatureCategories, opt => opt.MapFrom(src => src.RealEstateTypeFeatureCategories))
                .ReverseMap();
            CreateMap<DynamicFeature, DynamicFeatureDto>().ReverseMap();
            CreateMap<RealEstateFeatureValue, RealEstateFeatureValueDto>()
                .ForMember(dest => dest.FeatureId, opt => opt.MapFrom(src => src.FeatureId))
                .ReverseMap();
            CreateMap<RealEstateTypeFeature, RealEstateTypeFeatureDto>().ReverseMap();
            CreateMap<ApplicationUser, ProfileDto>().ReverseMap();
            CreateMap<FeatureCategory, FeatureCategoryDto>()
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.Features))
                .ReverseMap();

            CreateMap<Feature, FeatureDto>().ReverseMap();
            CreateMap<RealEstateFeature, RealEstateFeatureDto>().ReverseMap();
            CreateMap<RealEstateTypeFeatureCategory, RealEstateTypeFeatureCategoryDto>().ReverseMap();

           

        }
    }
}
