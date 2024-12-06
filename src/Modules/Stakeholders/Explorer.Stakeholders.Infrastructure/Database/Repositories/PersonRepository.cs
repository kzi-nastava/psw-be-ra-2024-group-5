using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class PersonRepository : CrudDatabaseRepository<Person, StakeholdersContext>, IPersonRepository
    {
        public PersonRepository(StakeholdersContext dbContext) : base(dbContext) { }

        public Person? GetByUserId(long userId)
        {
            return DbContext.People.FirstOrDefault(p => p.UserId == userId);
        }
    }
}
