using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
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
    }
}
