using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases;

public class EncounterService : CrudService<EncounterDto, Encounter>, IEncounterService {
     
    private readonly IEncounterRepository _encounterRepository;
    private readonly IMapper _mapper;

    public EncounterService(IEncounterRepository encounterRepository, IMapper mapper) 
        : base(encounterRepository, mapper) {

        _encounterRepository = encounterRepository;
        _mapper = mapper;
    }

    public Result<List<EncounterDto>> GetAllActive(long userId) {
        var encounters = _encounterRepository.GetAllActive();
        var encounterDtos = _mapper.Map<List<EncounterDto>>(encounters);
        return Result.Ok(encounterDtos);
    }

    public Result<List<EncounterDto>> GetByCreatorId(long creatorId) {
        var encounters = _encounterRepository.GetByCreatorId(creatorId);
        var encounterDtos = _mapper.Map<List<EncounterDto>>(encounters);
        return Result.Ok(encounterDtos);
    }
}
