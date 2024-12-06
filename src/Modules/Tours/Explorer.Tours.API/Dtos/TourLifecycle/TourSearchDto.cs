using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourLifecycle
{
    public class TourSearchDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public double? StartLat { get; set; }
        public double? EndLat { get; set; }
        public double? StartLong { get; set; }
        public double? EndLong { get; set; }

        public string? Name { get; set; }

        public double? Length { get; set; }
    }

}
