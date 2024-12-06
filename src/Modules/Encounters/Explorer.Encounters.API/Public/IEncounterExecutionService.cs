using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Enum;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public {
    public interface IEncounterExecutionService {
        public Result IsAvailable(EncounterExecutionRequestDto request);
        public Result<EncounterDto> GetActiveEncounter(long userId);
        public Result<EncounterDto> Start(EncounterExecutionRequestDto request);
        public Result Abandon(long userId);
        public Result<ProgressResponseDto> Progress(EncounterExecutionRequestDto request);
        public Result CompleteHiddenLocationEncounter(EncounterExecutionRequestDto request);
        public Result CompleteMiscEncounter(int encounterId, int userId);
    }
}
