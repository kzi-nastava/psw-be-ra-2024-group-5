using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Public
{
    public interface IParticipantService : ICrudService<ParticipantDto>
    {
        public Result<ParticipantDto> GetByUserId(long userId);
        public bool Exists(long userId);
        public void AddXP(long userId, int xp);
    }
}
