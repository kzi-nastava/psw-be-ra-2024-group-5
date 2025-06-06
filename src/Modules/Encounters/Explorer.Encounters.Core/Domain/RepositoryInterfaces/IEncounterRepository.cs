﻿using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain.RepositoryInterfaces;

public interface IEncounterRepository : ICrudRepository<Encounter>
{
    List<Encounter> GetAllActive();
    List<Encounter> GetByCreatorId(long creatorId);
    List<Encounter> GetAllDraft();
}