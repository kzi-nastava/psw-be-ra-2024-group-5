using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ClubRepository : CrudDatabaseRepository<Club, StakeholdersContext>, IClubRepository
    {
        private readonly StakeholdersContext _dbContext;
        public ClubRepository(StakeholdersContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Result CreateMembership(long clubId, long userId)
        {
            try
            {
                var memberships = _dbContext.Memberships;


                var membershipExists = memberships
                .Any(m => m.UserId == userId && m.ClubId == clubId);

                if (membershipExists)
                {
                    return Result.Fail("User is already in the club");
                }

                memberships.Add(new ClubMembership(clubId, userId));

                // Save changes to the database
                _dbContext.SaveChanges();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail($"Error occurred: {ex.Message}");
            }

        }

        public Result DeleteMembership(long clubId, long userId)
        {
            try
            {
                var memberships = _dbContext.Memberships;
                //TO CHANGE
                Result result = Result.Fail($"The specified user could not be found");
                foreach (var member in memberships)
                {
                    if (member.UserId == userId && member.ClubId == clubId)
                    {
                        memberships.Remove(member);
                        result = Result.Ok();
                        break;
                    }
                }
                _dbContext.SaveChanges();
                return result;

            }
            catch (Exception ex)
            {
                return Result.Fail($"Error occurred: {ex.Message}");
            }
        }
    }
}