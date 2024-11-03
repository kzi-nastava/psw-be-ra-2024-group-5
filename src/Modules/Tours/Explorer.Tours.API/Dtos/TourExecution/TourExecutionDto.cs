using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourExecution
{
    public class TourExecutionDto
    {
        public long Id { get; set; }
        public long TourId { get; set; }
        public ICollection<KeyPointProgressDto> KeyPointProgresses { get; set; } = new List<KeyPointProgressDto>();
    }
}
