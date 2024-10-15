using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Utilities;
using Microsoft.AspNetCore.Http;

public class ToursProfile : Profile {
    public ToursProfile() {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();

        CreateMap<KeyPointDto, KeyPoint>()
            .ConstructUsing(src => new KeyPoint(src.Name, src.Description, src.Latitude, src.Longitude, Base64Converter.ConvertToByteArray(src.Image), src.TourId))
            .ReverseMap()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Base64Converter.ConvertFromByteArray(src.Image)));
    }
}
