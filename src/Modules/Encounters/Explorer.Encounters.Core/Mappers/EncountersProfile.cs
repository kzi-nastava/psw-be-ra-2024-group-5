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
            .ReverseMap()
            .ForMember(dest => dest.CurrentPeopleCount, opt => opt.MapFrom(src => src.UserIds.Count));

        CreateMap<EncounterDto, Encounter>()
            .Include<SocialEncounterDto, SocialEncounter>().ReverseMap();


        CreateMap<ParticipantDto, Participant>().ReverseMap();
    }
}
