using AutoMapper;
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

public class EncounterService : IEncounterService {
     
    private readonly IEncounterRepository _encounterRepository;
    private readonly IMapper _mapper;

    public Result<EncounterDto> Create(EncounterDto encounterDto)
    {
        var encounter = _mapper.Map<Encounter>(encounterDto);
        _encounterRepository.Create(encounter);

        return Result.Ok(encounterDto);
    }

    public Result<bool> Delete(EncounterDto encounterDto)
    {
        _encounterRepository.Delete(encounterDto.Id);
        return true;
    }

    public Result<List<EncounterDto>> GetAllActive(long userId)
    {
        var encounters = _encounterRepository.GetAllActive();
        var encounterDtos = _mapper.Map<List<EncounterDto>>(encounters);
        return Result.Ok(encounterDtos);
    }

    public Result<EncounterDto> Update(EncounterDto encounterDto)
    {
        var encounter = _mapper.Map<Encounter>(encounterDto);
        try {
            _encounterRepository.Update(encounter);
        }
        catch (Exception e) {
            return Result.Fail(e.Message);
        }
        return Result.Ok(encounterDto);
    }
}
