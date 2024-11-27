using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
	public interface ICouponRepository : ICrudRepository<Coupon>
	{
		public Coupon GetByCode(string code);
		public void Add(Coupon coupon);
		public void SaveChanges();
		public Coupon GetById(long id);
		public void Delete(Coupon coupon);
		public void Update(Coupon coupon);
		public List<Coupon> GetAll();

	}
}
