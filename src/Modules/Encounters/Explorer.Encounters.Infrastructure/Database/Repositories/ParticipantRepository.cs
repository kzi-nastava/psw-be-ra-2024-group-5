using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Infrastructure.Database.Repositories;
public class ParticipantRepository : CrudDatabaseRepository<Participant, EncountersContext>, IParticipantRepository
{
    public ParticipantRepository(EncountersContext dbContext) : base(dbContext) { }

    public Participant GetByUserId(long userId)
    {
        return DbContext.Set<Participant>().FirstOrDefault(p=> p.UserId == userId);
    }
}
