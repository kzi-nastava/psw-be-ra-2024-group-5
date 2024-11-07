using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Dtos.Messages;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Messages;

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
            .ForMember(dest => dest.Motto, opt => opt.MapFrom(src => src.Item1.Motto))
            .ForMember(dest => dest.Messages, opt => opt.Ignore());

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
            CreateMap<ClubMembershipDto, ClubMembership>().ReverseMap();
            CreateMap<AppRatingDto, AppRating>().ReverseMap();

        CreateMap<Person, UserProfileDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.ProfilePictureUrl, opt => opt.Ignore())
            .ForMember(dest => dest.Biography, opt => opt.Ignore())
            .ForMember(dest => dest.Motto, opt => opt.Ignore());

        CreateMap<FollowingDto, Following>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.FollowedUserId, opt => opt.MapFrom(src => src.FollowedUserId));

        CreateMap<Following, FollowingDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.FollowedUserId, opt => opt.MapFrom(src => src.FollowedUserId));

        CreateMap<ClubMessage, ClubMessageDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.SenderId))
                .ForMember(dest => dest.SentAt, opt => opt.MapFrom(src => src.SentAt))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.IsRead, opt => opt.MapFrom(src => src.IsRead))
                .ForMember(dest => dest.Attachment, opt => opt.MapFrom(src =>
                    src.Attachment == null ? null : new AttachmentDto(
                        src.Attachment.ResourceId,
                        (int)src.Attachment.ResourceType)));

        CreateMap<ClubMessageDto, ClubMessage>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.SenderId))
            .ForMember(dest => dest.SentAt, opt => opt.MapFrom(src => src.SentAt))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.IsRead, opt => opt.MapFrom(src => src.IsRead))
            .ForMember(dest => dest.Attachment, opt => opt.MapFrom(src =>
                src.Attachment == null ? null : new Attachment(
                    src.Attachment.ResourceId,
                    (ResourceType)src.Attachment.ResourceType)));

        CreateMap<Attachment, AttachmentDto>()
            .ForMember(dest => dest.ResourceId, opt => opt.MapFrom(src => src.GetResourceId()))
            .ForMember(dest => dest.ResourceType, opt => opt.MapFrom(src => (int)src.GetResourceType()));

        CreateMap<AttachmentDto, Attachment>()
            .ForMember(dest => dest.ResourceId, opt => opt.MapFrom(src => src.ResourceId))
            .ForMember(dest => dest.ResourceType, opt => opt.MapFrom(src => (ResourceType)src.ResourceType));

    }
}