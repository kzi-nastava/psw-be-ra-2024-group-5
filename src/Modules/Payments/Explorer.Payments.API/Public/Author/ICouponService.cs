using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public.Author
{
	public interface ICouponService
	{
		Task<Result<CouponDto>> Create(CouponDto couponDto);
		public Result Delete(long id);
		public Result<CouponDto> Update(long id, CouponDto couponDto);
		public Result<List<CouponDto>> GetAll();


	}
}
