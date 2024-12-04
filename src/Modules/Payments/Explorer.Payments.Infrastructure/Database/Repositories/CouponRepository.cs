using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
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
		/*public async Task<Coupon> GetByCode(string code)
		{
			return await _context.Coupons.FirstOrDefaultAsync(c => c.Code == code);
		}*/

		public void Add(Coupon coupon)
		{
			_context.Coupons.Add(coupon);
		}
	/*	public async Task AddAsync(Coupon coupon)
		{
			await _context.Coupons.AddAsync(coupon);
		}*/
		public void SaveChanges()
		{
			foreach (var entry in _context.ChangeTracker.Entries())
			{
				foreach (var property in entry.Properties)
				{
					if (property.CurrentValue is DateTime dateTimeValue && dateTimeValue.Kind == DateTimeKind.Unspecified)
					{
						property.CurrentValue = DateTime.SpecifyKind(dateTimeValue, DateTimeKind.Utc);
					}
				}
			}

			_context.SaveChanges();
		}
		//dodala ja-proba
	/*	public async Task SaveChangesAsync()
		{
			foreach (var entry in _context.ChangeTracker.Entries())
			{
				foreach (var property in entry.Properties)
				{
					if (property.CurrentValue is DateTime dateTimeValue && dateTimeValue.Kind == DateTimeKind.Unspecified)
					{
						property.CurrentValue = DateTime.SpecifyKind(dateTimeValue, DateTimeKind.Utc);
					}
				}
			}

			await _context.SaveChangesAsync();
		}*/
		public Coupon GetById(long id)
		{
			return _context.Coupons.FirstOrDefault(c => c.Id == id);
		}
		public void Delete(Coupon coupon)
		{
			_context.Coupons.Remove(coupon);
		}
		public void Update(Coupon coupon)
		{
			_context.Coupons.Update(coupon);
		}
		public List<Coupon> GetAll()
		{
			return _context.Coupons.ToList(); 
		}

	}
}
