using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Internal;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.UseCases
{
    public class ParticipantService : CrudService<ParticipantDto, Participant>, IParticipantService, IInternalParticipantService
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IMapper _mapper;

        public ParticipantService(IParticipantRepository participantRepository, IMapper mapper)
            : base(participantRepository, mapper)
        {
            _participantRepository = participantRepository;
            _mapper = mapper;
        }

        public Result<ParticipantDto> GetByUserId(long userId)
        {
            var participant = _participantRepository.GetByUserId(userId);
            ParticipantDto participantDto;

            if(participant == null)
            {
                participantDto = new ParticipantDto(userId, 0, 0);
            } else
            {
                participantDto = _mapper.Map<ParticipantDto>(participant);
            }
            return Result.Ok(participantDto);
        }

        public bool Exists(long userId)
        {
            return _participantRepository.Exists(userId);
        }

        public void AddXP(long userId, int xp)
        {
            var participant = _participantRepository.GetByUserId(userId);
            participant.AddXP(xp);
            _participantRepository.Update(participant);
        }
    }
}
