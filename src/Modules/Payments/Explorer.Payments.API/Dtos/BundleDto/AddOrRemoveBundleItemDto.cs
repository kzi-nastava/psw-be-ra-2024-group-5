using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos.BundleDto
{
    public class AddOrRemoveBundleItemDto
    {
        public long Id { get; set; }
        public long AuthorId { get; set; }
        public long BundleId { get; set; }
    }
}
