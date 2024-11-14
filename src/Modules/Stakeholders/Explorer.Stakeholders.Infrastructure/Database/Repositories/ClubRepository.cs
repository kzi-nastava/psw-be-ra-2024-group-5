using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ClubRepository : CrudDatabaseRepository<Club, StakeholdersContext>, IClubRepository
    {
        public ClubRepository(StakeholdersContext dbContext) : base(dbContext) { }

        public new Club Get(long clubId)
        {
            var result = DbContext.Clubs.Where(c => c.Id == clubId)
                .Include(c => c.ClubMessages).FirstOrDefault();

            if (result == null)
                throw new KeyNotFoundException("Club not found: " + clubId);
            else return result;
        }

        public new Club Update(Club club)
        {
            try
            {
                DbContext.Entry(club).State = EntityState.Modified;
                DbContext.SaveChanges();
                return club;
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
        }

        public List<ClubMembership> GetAllMemberships()
        {
            return DbContext.Memberships.ToList();
        }

        public ClubMembership? CreateMembership(long clubId, long userId)
        {
            var memberships = DbContext.Memberships;


            var membershipExists = memberships
            .Any(m => m.UserId == userId && m.ClubId == clubId);

            if (membershipExists)
            {
                return null;
            }

            var newMembership = new ClubMembership(clubId, userId);

            memberships.Add(newMembership);
            DbContext.SaveChanges();
            return newMembership;
        }

        public bool DeleteMembership(long clubId, long userId)
        {
            var memberships = DbContext.Memberships;

            var membership = memberships.FirstOrDefault(m => m.UserId == userId && m.ClubId == clubId);

            if (membership == null)
            {
                return false; // Membership not found
            }

            memberships.Remove(membership);
            DbContext.SaveChanges();
            return true;
        }
    }
}