using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos.BundleDto
{
    public class BundleSummaryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ShoppingMoneyDto Price { get; set; }
        public string Status { get; set; }
    }
}
