using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class AppRatingDatabaseRepository : IAppRatingRepository
    {
        private readonly StakeholdersContext _dbContext;

        public AppRatingDatabaseRepository(StakeholdersContext dbContext)
        {
            _dbContext = dbContext;
        }

        public AppRating GetByUser(long userId)
        {
            return _dbContext.AppRating.FirstOrDefault(u => u.UserId == userId);
        }
    }
}
