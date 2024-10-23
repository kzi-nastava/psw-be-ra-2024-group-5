using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.Mappers;

public class StakeholderProfile : Profile
{
    public StakeholderProfile()
    {
        CreateMap<Person, AccountDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

        CreateMap<User, AccountDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.GetPrimaryRoleName()))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

        CreateMap<(UserProfile, Person), UserProfileDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item1.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Item1.UserId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Item2.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Item2.Surname))
            .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.Item1.ProfilePictureUrl))
            .ForMember(dest => dest.Biography, opt => opt.MapFrom(src => src.Item1.Biography))
            .ForMember(dest => dest.Motto, opt => opt.MapFrom(src => src.Item1.Motto));

        CreateMap<UserProfileDto, UserProfile>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePictureUrl))
            .ForMember(dest => dest.Biography, opt => opt.MapFrom(src => src.Biography))
            .ForMember(dest => dest.Motto, opt => opt.MapFrom(src => src.Motto));

        CreateMap<UserProfileDto, Person>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname));

        CreateMap<ClubDto, Club>().ReverseMap();

        CreateMap<AppRatingDto, AppRating>().ReverseMap();
    }
}