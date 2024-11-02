using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourExecution
{
    public class KeyPointProgressDto
    {
        public KeyPointDto KeyPoint { get; set; }
        public DateTime? VisitTime { get; set; }
    }
}
