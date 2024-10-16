using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Utilities;
using Microsoft.AspNetCore.Http;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<TourDto, Tour>().ReverseMap();
        CreateMap<TourTagDto, TourTag>().ReverseMap();
        CreateMap<EquipmentDto, Equipment>().ReverseMap();
        
        CreateMap<KeyPointDto, KeyPoint>()
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("description", opt => opt.MapFrom(src => src.Description))
            .ForCtorParam("latitude", opt => opt.MapFrom(src => src.Latitude))
            .ForCtorParam("longitude", opt => opt.MapFrom(src => src.Longitude))
            .ForCtorParam("image", opt => opt.MapFrom(src => Base64Converter.ConvertToByteArray(src.Image)))
            .ForCtorParam("tourId", opt => opt.MapFrom(src => src.TourId))
            .ReverseMap()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Base64Converter.ConvertFromByteArray(src.Image)));

        CreateMap<FacilityDto, Facility>()
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("description", opt => opt.MapFrom(src => src.Description))
            .ForCtorParam("longitude", opt => opt.MapFrom(src => src.Longitude))
            .ForCtorParam("latitude", opt => opt.MapFrom(src => src.Latitude))
            .ForCtorParam("image", opt => opt.MapFrom(src => Base64Converter.ConvertToByteArray(src.Image)))
            .ForCtorParam("type", opt => opt.MapFrom(src => src.Type))
            .ReverseMap()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Base64Converter.ConvertFromByteArray(src.Image)));
    }
}
