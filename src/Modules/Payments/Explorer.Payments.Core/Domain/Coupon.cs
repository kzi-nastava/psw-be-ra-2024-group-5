using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
	public class Coupon : Entity
	{
		[MaxLength(8)]
		public string Code { get; set; } = Guid.NewGuid().ToString().Substring(0, 8);

		[Range(1, 100)]
		public long Percentage { get; set; }
		public DateTime ExpiredDate { get; set; }
		public List<long> TourIds { get; set; }

		public Coupon() { }

		public Coupon(string code, long percentage, DateTime expiredDate, List<long> tourIds)
		{
			Code = code;
			Percentage = percentage;
			ExpiredDate = expiredDate;
			TourIds = tourIds;
		}
	}
}
