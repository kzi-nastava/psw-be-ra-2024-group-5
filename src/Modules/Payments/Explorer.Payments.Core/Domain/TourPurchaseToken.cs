﻿using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain
{
	public class TourPurchaseToken : Entity
	{
		public long TourId { get; set; }
		public long UserId { get; set; }
		public DateTime PurchaseDate { get; set; }
	}
}
