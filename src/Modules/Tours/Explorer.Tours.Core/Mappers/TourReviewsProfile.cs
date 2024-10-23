using AutoMapper;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Utilities;

namespace Explorer.Tours.Core.Mappers
{
    public class TourReviewsProfile : Profile
    {
        public TourReviewsProfile()
        {
            CreateMap<TourReviewDto, TourReview>().ReverseMap();
            CreateMap<TourReviewDto, TourReview>()
    .ForCtorParam("rating", opt => opt.MapFrom(src => src.Rating))
    .ForCtorParam("comment", opt => opt.MapFrom(src => src.Comment))
    .ForCtorParam("visitDate", opt => opt.MapFrom(src => src.VisitDate))
    .ForCtorParam("reviewDate", opt => opt.MapFrom(src => src.ReviewDate))
    .ForCtorParam("image", opt => opt.MapFrom(src => Base64Converter.ConvertToByteArray(src.Image)))
    .ForCtorParam("tourId", opt => opt.MapFrom(src => src.TourId))
    .ForCtorParam("touristId", opt => opt.MapFrom(src => src.TouristId))
    .ReverseMap()
    .ForMember(dest => dest.Image, opt => opt.MapFrom(src => Base64Converter.ConvertFromByteArray(src.Image)));
        }
    }
}
