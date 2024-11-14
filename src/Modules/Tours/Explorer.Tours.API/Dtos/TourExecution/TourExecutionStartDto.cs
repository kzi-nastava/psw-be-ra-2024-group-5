using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourExecution {
    public class TourExecutionStartDto {
        public long UserId { get; set; }
        public long TourId { get; set; }
    }
}
