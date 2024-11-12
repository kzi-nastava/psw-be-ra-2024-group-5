using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces {
    public interface ITourExecutionRepository : ICrudRepository<TourExecution> {
        public new TourExecution Update(TourExecution entity);
        public new TourExecution Get(long id);
        public TourExecution GetActive(long userId);
        public List<TourExecution> GetRecentByTourAndUser(long tourId, long userId);
    }
}
