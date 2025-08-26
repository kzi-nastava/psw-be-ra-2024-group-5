using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Mappers;

public class EncountersProfile : Profile
{
    public EncountersProfile()
    {
        CreateMap<LocationDto, Location>().ReverseMap();

        CreateMap<SocialEncounterDto, SocialEncounter>()
            .ForMember(dest => dest.UserIds, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.CurrentPeopleCount, opt => opt.MapFrom(src => src.UserIds.Count));

        CreateMap<HiddenLocationEncounterDto, HiddenLocationEncounter>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ConvertToByteArray(src.Image)))
            .ReverseMap()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ConvertFromByteArray(src.Image)));

        CreateMap<RiddleEncounterDto, RiddleEncounter>()
            .ForMember(dest => dest.Riddle, opt => opt.MapFrom(src => src.Riddle))
            .ForMember(dest => dest.PotentialAnswers, opt => opt.MapFrom(src => src.PotentialAnswers))
            .ReverseMap()
            .ForMember(dest => dest.Riddle, opt => opt.MapFrom(src => src.Riddle))
            .ForMember(dest => dest.PotentialAnswers, opt => opt.MapFrom(src => src.PotentialAnswers));

        CreateMap<EncounterDto, Encounter>()
            .Include<SocialEncounterDto, SocialEncounter>()
            .Include<HiddenLocationEncounterDto, HiddenLocationEncounter>()
            .Include<RiddleEncounterDto, RiddleEncounter>()
            .ReverseMap()
            .Include<SocialEncounter, SocialEncounterDto>()
            .Include<HiddenLocationEncounter, HiddenLocationEncounterDto>()
            .Include<RiddleEncounter, RiddleEncounterDto>();

        CreateMap<ParticipantDto, Participant>()
            .ReverseMap();
    }

    public static byte[] ConvertToByteArray(string? base64Image) {
        try {
            return Convert.FromBase64String(base64Image);
        }
        catch (FormatException ex) {
            Console.WriteLine("Invalid Base64 string: " + ex.Message);
            return null;
        }
    }

    public static string ConvertFromByteArray(byte[]? image) {
        return image == null ? null : Convert.ToBase64String(image);
    }
}
