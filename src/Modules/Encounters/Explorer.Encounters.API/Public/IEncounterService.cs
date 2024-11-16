using Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public;
public interface IEncounterService
{
    public Result<EncounterDto> Create(EncounterDto encounterDto);
    public Result<EncounterDto> Update(EncounterDto encounterDto);
    public Result<bool> Delete(long id);
    public Result<List<EncounterDto>> GetAllActive(long userId);
    public Result<List<EncounterDto>> GetByCreatorId(long creatorId);
}
