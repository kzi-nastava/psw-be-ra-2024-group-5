using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain {
    public class TransportDuration : ValueObject {
        public double Duration { get;  }
        public TourTransport Transport { get; }

        private TransportDuration() { }

        [JsonConstructor]
        public TransportDuration(double duration, TourTransport transport) {
            Duration = duration;
            Transport = transport;
        }

        protected override IEnumerable<object> GetEqualityComponents() {
            yield return Duration;
            yield return Transport;
        }

        
    }
}
