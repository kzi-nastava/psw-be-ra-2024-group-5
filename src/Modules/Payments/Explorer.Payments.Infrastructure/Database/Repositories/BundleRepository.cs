using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class BundleRepository : CrudDatabaseRepository<Bundle, PaymentsContext>, IBundleRepository
    {
        public BundleRepository(PaymentsContext dbContext) : base(dbContext){}

        public new Bundle Update(Bundle aggregateRoot)
        {
            DbContext.Entry(aggregateRoot).State = EntityState.Modified;
            DbContext.SaveChanges();
            return aggregateRoot;
        }

        public List<Bundle> GetBundlesPublished(int page, int pageSize) {
            if (page < 1 || pageSize < 1) {
                return new List<Bundle>();
            }

            return DbContext.Bundles
                .Where(t => t.Status == API.Enum.BundleStatus.Published)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
