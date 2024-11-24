using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifecycle;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Utilities;

namespace Explorer.Tours.Core.Mappers;

public class ToursProfile : Profile
{
    public ToursProfile()
    {
        CreateMap<MoneyDto, Money>().ReverseMap();

        CreateMap<TransportDurationDto, TransportDuration>().ReverseMap();

        CreateMap<KeyPointDto, KeyPoint>()
            .ForCtorParam("image", opt => opt.MapFrom(src => Base64Converter.ConvertToByteArray(src.Image)))
            .ReverseMap()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Base64Converter.ConvertFromByteArray(src.Image)));


        CreateMap<TourReviewDto, TourReview>()
            .ForCtorParam("image", opt => opt.MapFrom(src => Base64Converter.ConvertToByteArray(src.Image)))
            .ReverseMap()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Base64Converter.ConvertFromByteArray(src.Image)));


        CreateMap<FacilityDto, Facility>()
            .ForCtorParam("image", opt => opt.MapFrom(src => Base64Converter.ConvertToByteArray(src.Image)))
            .ReverseMap()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Base64Converter.ConvertFromByteArray(src.Image)));


        CreateMap<EquipmentDto, Equipment>().ReverseMap();

        CreateMap<PreferenceDto, Preference>().ReverseMap();

        CreateMap<TourCreationDto, Tour>();

        CreateMap<Tour, TourCardDto>();

        CreateMap<TourDto, Tour>().ReverseMap();
    }
}

