using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ClaimedRewardDto
    {
        public long? TourId { get; set; }
        public string? TourName { get; set; }
        public string? Image { get; set; }
        public long? CouponId { get; set; }
        public string? Code { get; set; }
        public long? Percentage { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public ClaimedRewardDto() { }
    }
}
