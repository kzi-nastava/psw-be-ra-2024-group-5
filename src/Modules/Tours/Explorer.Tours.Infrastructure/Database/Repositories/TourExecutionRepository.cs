using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.API.Enum;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories {
    public class TourExecutionRepository : CrudDatabaseRepository<TourExecution, ToursContext>, ITourExecutionRepository {
        public TourExecutionRepository(ToursContext dbContext) : base(dbContext) { }

        public new TourExecution Update(TourExecution entity) {
            DbContext.Entry(entity).State = EntityState.Modified;
            DbContext.SaveChanges();
            return entity;
        }

        public new TourExecution Get(long id) {
            var tourExecution = DbContext.TourExecutions.Where(te => te.Id == id)
                                                        .Include(te => te.KeyPointProgresses)
                                                        .FirstOrDefault();
            if (tourExecution == null)
                throw new KeyNotFoundException("Not found: " + id);

            return tourExecution;
        }

        public TourExecution GetActive(long userId) {
            var tourExecution = DbContext.TourExecutions.Where(te => te.UserId == userId && te.Status == TourExecutionStatus.Active)
                                                        .Include(te => te.KeyPointProgresses)
                                                        .ThenInclude(kpp => kpp.KeyPoint)
                                                        .FirstOrDefault();
            return tourExecution;
        }

        public TourExecution GetByTourAndUser(long tourId, long userId)
        {
            throw new NotImplementedException();
        }
    }
}
