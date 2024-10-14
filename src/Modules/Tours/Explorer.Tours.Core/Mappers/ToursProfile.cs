using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Microsoft.AspNetCore.Http;

public class ToursProfile : Profile {
    public ToursProfile() {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();

        CreateMap<KeyPointDto, KeyPoint>()
            .ForCtorParam("name", opt => opt.MapFrom(src => src.Name))
            .ForCtorParam("description", opt => opt.MapFrom(src => src.Description))
            .ForCtorParam("latitude", opt => opt.MapFrom(src => src.Latitude))
            .ForCtorParam("longitude", opt => opt.MapFrom(src => src.Longitude))
            .ForCtorParam("image", opt => opt.MapFrom(src => ConvertToByteArray(src.Image)))
            .ReverseMap()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ConvertToBase64String(src.Image)));

    }

    private byte[] ConvertToByteArray(string base64Image) {
        return string.IsNullOrEmpty(base64Image) ? null : Convert.FromBase64String(base64Image);
    }

    private string ConvertToBase64String(byte[] image) {
        return image == null ? null : Convert.ToBase64String(image);
    }
}
