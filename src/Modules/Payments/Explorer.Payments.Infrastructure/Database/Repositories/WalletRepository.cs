using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class WalletRepository : CrudDatabaseRepository<Wallet, PaymentsContext>, IWalletRepository
    {
        public WalletRepository(PaymentsContext paymentsContext) : base(paymentsContext) {  }

        public Wallet GetByTouristId(long touristId)
        {
            return DbContext.Wallets.Where(w => w.TouristId == touristId).FirstOrDefault();
        }
    }
}
