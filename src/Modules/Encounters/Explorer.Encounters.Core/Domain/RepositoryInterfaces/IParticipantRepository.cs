﻿using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces;

public interface IParticipantRepository : ICrudRepository<Participant>
{
    Participant GetByUserId(long userId);
    public bool Exists(long userId);
}
