using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos.BundleDto
{
    public class BundleDetailsDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ShoppingMoneyDto Price { get; set; }
        public long AuthorId { get; set; }
        public IEnumerable<long> BundleItems { get; set; }
        public string Status { get; set; }
    }
}
