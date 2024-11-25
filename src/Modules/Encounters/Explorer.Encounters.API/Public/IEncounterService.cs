using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public;
public interface IEncounterService : ICrudService<EncounterDto> {
    public Result<List<EncounterDto>> GetAllActive();
    public Result<List<EncounterDto>> GetByCreatorId(long creatorId);
    public Result<EncounterDto> Create(EncounterDto encounter);
    public Result<List<EncounterDto>> GetAllDraft();
    public Result AcceptEncounter(long encounterId);
    public Result RejectEncounter(long encounterId);
}
