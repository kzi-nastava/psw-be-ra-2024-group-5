using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<TourDto, Tour>().ReverseMap();
        CreateMap<TourTagDto, TourTag>().ReverseMap();
        CreateMap<EquipmentDto, Equipment>().ReverseMap();
    }
}