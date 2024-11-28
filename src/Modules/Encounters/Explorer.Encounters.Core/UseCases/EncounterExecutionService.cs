using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Enum;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases {
    public class EncounterExecutionService : IEncounterExecutionService {
        private readonly IEncounterExecutionRepository _executionRepository;
        private readonly IEncounterRepository _encounterRepository;
        private readonly IMapper _mapper;

        public EncounterExecutionService(IEncounterExecutionRepository executionRepository, IEncounterRepository encounterRepository, IMapper mapper) {
            _executionRepository = executionRepository;
            _encounterRepository = encounterRepository;
            _mapper = mapper;
        }

        public Result IsAvailable(EncounterExecutionRequestDto request) {
            try {
                var encounter = _encounterRepository.Get(request.EncounterId);

                if (!encounter.IsClose(request.Location))
                    return Result.Fail(FailureCode.InvalidArgument).WithError("You are too far away!");
                else if (IsAlreadyOnEncounter(request.UserId))
                    return Result.Fail(FailureCode.Conflict).WithError("Finish or abandon your current encounter to start a new one!");
                else
                    return Result.Ok();
            }
            catch {
                return Result.Fail(FailureCode.NotFound).WithError("Encounter not found.");
            }
        }

        private bool IsAlreadyOnEncounter(long userId) {
            return _executionRepository.GetActive(userId) != null;
        }

        public Result<EncounterDto> GetActiveEncounter(long userId) {
            var execution = _executionRepository.GetActive(userId);
            
            if (execution == null)
                return Result.Ok();

            var encounter = _encounterRepository.Get(execution.EncounterId);
            return Result.Ok(_mapper.Map<EncounterDto>(encounter));
        }

        public Result<EncounterDto> Start(EncounterExecutionRequestDto request) {
            try {
                var encounter = _encounterRepository.Get(request.EncounterId);

                if(IsAvailable(request).IsFailed)
                    return Result.Fail(FailureCode.InvalidArgument).WithError("Unavailable!");

                _executionRepository.Create(new EncounterExecution(request.UserId, request.EncounterId));
                return Result.Ok(_mapper.Map<EncounterDto>(encounter));
            }
            catch {
                return Result.Fail(FailureCode.NotFound).WithError("Encounter not found.");
            }
        }

        public Result<ProgressResponseDto> Progress(EncounterExecutionRequestDto request) {
            try {
                var encounterExecution = _executionRepository.GetActive(request.UserId);

                if (encounterExecution == null)
                    return Result.Fail(FailureCode.NotFound).WithError("Execution not found.");

                var encounter = _encounterRepository.Get(encounterExecution.EncounterId);

                if (encounter.Id != request.EncounterId)
                    return Result.Fail(FailureCode.InvalidArgument);

                switch (encounter.Type) {
                    case EncounterType.Social:
                        ProgressSocialEncounter(request, (SocialEncounter)encounter);
                        break;
                    case EncounterType.Locaion:
                        return Result.Ok(ProgressHidden(request.Location, (HiddenLocationEncounter)encounter, encounterExecution));
                    default:
                        break;
                }

                return Result.Ok();
            } catch {
                return Result.Fail(FailureCode.NotFound).WithError("Encounter not found.");
            }
        }
        
        public void ProgressSocialEncounter(EncounterExecutionRequestDto request, SocialEncounter encounter) {
            if(encounter.CheckUserNearby(request.Location)) {
                encounter.AddUser(request.UserId);
            }
            else {
                encounter.RemoveUser(request.UserId);
            }

            _encounterRepository.Update(encounter);

            if (!encounter.CanBeCompletedByUser(request.UserId))
                return; // Here we return something

            foreach (var userId in encounter.UserIds) {
                var execution = _executionRepository.GetActive(userId);
                if (execution == null)
                    continue;

                execution.Complete();
                _executionRepository.Update(execution);
            }

            encounter.Complete();
            _encounterRepository.Update(encounter);
        }

        public Result Abandon(long userId) {
            var encounterExecution = _executionRepository.GetActive(userId);
            if (encounterExecution == null)
                return Result.Fail(FailureCode.NotFound).WithError("Execution not found.");

            encounterExecution.Abandon();
            _executionRepository.Update(encounterExecution);

            return Result.Ok();
        }
        private ProgressResponseDto ProgressHidden(Location location, HiddenLocationEncounter encounter, EncounterExecution execution) {
            if (!encounter.IsCloseToImageLocation(location))
                return new ProgressResponseDto(false);

            execution.Progress();
            _executionRepository.Update(execution);
            return new ProgressResponseDto(true);
        }

        public Result CompleteHiddenLocationEncounter(EncounterExecutionRequestDto request) {
            try {
                var encounterExecution = _executionRepository.GetActive(request.UserId);

                if (encounterExecution == null)
                    return Result.Fail(FailureCode.NotFound).WithError("Execution not found.");

                var encounter = (HiddenLocationEncounter)_encounterRepository.Get(encounterExecution.EncounterId);

                if (encounter.Id != request.EncounterId)
                    return Result.Fail(FailureCode.InvalidArgument);

                if (encounter.IsCloseToImageLocation(request.Location))
                    if((DateTime.UtcNow - encounterExecution.LastActivity).TotalSeconds >= 30) {
                        encounterExecution.Complete();
                        _executionRepository.Update(encounterExecution);
                        return Result.Ok();
                    }

                return Result.Fail(FailureCode.InvalidArgument);
            }
            catch {
                return Result.Fail(FailureCode.NotFound).WithError("Encounter not found.");
            }
        }
    }
}
