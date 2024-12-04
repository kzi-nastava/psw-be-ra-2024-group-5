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
		//Task<Coupon> GetByCode(string code);
		public void Add(Coupon coupon);
		//Task AddAsync(Coupon coupon);

		public void SaveChanges();
		//Task SaveChangesAsync();

		public Coupon GetById(long id);
		public void Delete(Coupon coupon);
		public void Update(Coupon coupon);
		public List<Coupon> GetAll();

	}
}
