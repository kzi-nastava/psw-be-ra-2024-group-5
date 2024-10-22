using AutoMapper.Execution;
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

        public ClubMembership? CreateMembership(long clubId, long userId)
        {
            var memberships = _dbContext.Memberships;


            var membershipExists = memberships
            .Any(m => m.UserId == userId && m.ClubId == clubId);

            if (membershipExists)
            {
                return null;
            }

            var newMembership = new ClubMembership(clubId, userId);

            memberships.Add(newMembership);
            _dbContext.SaveChanges();
            return newMembership;
        }

        public bool DeleteMembership(long clubId, long userId)
        {
            var memberships = _dbContext.Memberships;

            var membership = memberships.FirstOrDefault(m => m.UserId == userId && m.ClubId == clubId);

            if (membership == null)
            {
                return false; // Membership not found
            }

            memberships.Remove(membership);
            _dbContext.SaveChanges();
            return true;      
        }
    }
}