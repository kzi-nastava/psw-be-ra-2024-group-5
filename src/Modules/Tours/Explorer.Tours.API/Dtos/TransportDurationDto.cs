using Explorer.Tours.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos {
    public  class TransportDurationDto {
        public double Duration { get; set; }
        public TourTransport Transport { get; set; }

        public TransportDurationDto() { }
        public TransportDurationDto(double duration, TourTransport transport) {
            Duration = duration;
            Transport = transport;
        }
    }
}
