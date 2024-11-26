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
	public class CouponRepository : CrudDatabaseRepository<Coupon, PaymentsContext>, ICouponRepository
	{
		private readonly PaymentsContext _context;

		public CouponRepository(PaymentsContext context) : base(context)
		{
			_context = context;
		}
		public Coupon GetByCode(string code)
		{
			return _context.Coupons.FirstOrDefault(c => c.Code == code);
		}

		public void Add(Coupon coupon)
		{
			_context.Coupons.Add(coupon);
		}
		public void SaveChanges()
		{
			_context.SaveChanges();
		}

	}
}
