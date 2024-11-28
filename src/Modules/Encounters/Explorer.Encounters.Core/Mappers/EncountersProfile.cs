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
        CreateMap<SocialEncounterDto, SocialEncounter>().ReverseMap();
        CreateMap<HiddenLocationEncounterDto, HiddenLocationEncounter>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ConvertToByteArray(src.Image)))
            .ReverseMap()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ConvertFromByteArray(src.Image)));

        CreateMap<EncounterDto, Encounter>()
            .Include<SocialEncounterDto, SocialEncounter>().ReverseMap();

        CreateMap<EncounterDto, Encounter>()
            .Include<HiddenLocationEncounterDto, HiddenLocationEncounter>().ReverseMap();

        CreateMap<ParticipantDto, Participant>().ReverseMap();
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
