using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Enum;
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
    
}
