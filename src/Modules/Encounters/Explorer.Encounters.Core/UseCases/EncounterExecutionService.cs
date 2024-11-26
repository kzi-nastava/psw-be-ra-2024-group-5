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
        private readonly IParticipantService _participantService;

        public EncounterExecutionService(IEncounterExecutionRepository executionRepository,
            IEncounterRepository encounterRepository, IMapper mapper, IParticipantService participantService)
        {
            _executionRepository = executionRepository;
            _encounterRepository = encounterRepository;
            _mapper = mapper;
            _participantService = participantService;
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

                if(!_participantService.Exists(request.UserId))
                    _participantService.Create(new ParticipantDto(request.UserId, 0, 0));

                _executionRepository.Create(new EncounterExecution(request.UserId, request.EncounterId));
                return Result.Ok(_mapper.Map<EncounterDto>(encounter));
            }
            catch {
                return Result.Fail(FailureCode.NotFound).WithError("Encounter not found.");
            }
        }

        public Result Progress(EncounterExecutionRequestDto request) {
            try {
                var encounterExecution = _executionRepository.GetActive(request.UserId);

                if (encounterExecution == null)
                    return Result.Fail(FailureCode.NotFound).WithError("Execution not found.");

                var encounter = _encounterRepository.Get(encounterExecution.EncounterId);

                if (encounter.Id != request.EncounterId)
                    return Result.Fail(FailureCode.InvalidArgument);

                switch (encounter.Type) {
                    case EncounterType.Social:
                        break;
                    case EncounterType.Locaion:
                        break;
                    default:
                        break;
                }

                return Result.Ok();
            } catch {
                return Result.Fail(FailureCode.NotFound).WithError("Encounter not found.");
            }
        }
    }
}
