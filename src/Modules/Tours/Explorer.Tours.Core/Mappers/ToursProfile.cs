using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Microsoft.AspNetCore.Http;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<EquipmentDto, Equipment>().ReverseMap();
        CreateMap<KeyPointDto, KeyPoint>().ForMember(dest => dest.Image, opt => opt.MapFrom(src => ConvertToByteArray(src.Image)));
    }

    private byte[] ConvertToByteArray(IFormFile imageFile) {
        if (imageFile == null) 
            return null;

        using (var memoryStream = new MemoryStream()) {
            imageFile.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}