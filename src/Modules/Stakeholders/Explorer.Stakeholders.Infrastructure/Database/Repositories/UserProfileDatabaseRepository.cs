using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories;

public class UserProfileDatabaseRepository
    : CrudDatabaseRepository<UserProfile, StakeholdersContext>, IUserProfileRepository
{
    public UserProfileDatabaseRepository(StakeholdersContext dbContext) : base(dbContext) { }

    public new UserProfile? Get(long profileId)
    {
        return DbContext.Profiles.Where(p => p.Id == profileId)
            .Include(p => p.Messages).FirstOrDefault();
    }

    public new UserProfile Update(UserProfile userProfile)
    {
        DbContext.Entry(userProfile).State = EntityState.Modified;
        DbContext.SaveChanges();
        return userProfile;
    }
}