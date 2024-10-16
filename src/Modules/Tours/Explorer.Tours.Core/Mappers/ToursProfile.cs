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
        CreateMap<TourDto, Tour>()
            .ForMember(dest => dest.Status, opt => opt.Ignore())
            .ForMember(dest => dest.Price, opt => opt.Ignore())
            .ReverseMap();
        CreateMap<EquipmentDto, Equipment>().ReverseMap();
        
        CreateMap<KeyPointDto, KeyPoint>()
            .ConstructUsing(src => new KeyPoint(src.Name, src.Description, src.Latitude, src.Longitude, Base64Converter.ConvertToByteArray(src.Image), src.TourId))
            .ReverseMap()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Base64Converter.ConvertFromByteArray(src.Image)));
    
    }
}
