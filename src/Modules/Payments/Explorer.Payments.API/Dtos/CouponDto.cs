using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
	public class CouponDto
	{
		public long Id { get; set; }
		public string Code { get;  set; }
		public long Percentage { get;  set; }
		public DateTime ExpiredDate { get; set; }
		public List<long> TourIds { get;  set; }

	}
}
