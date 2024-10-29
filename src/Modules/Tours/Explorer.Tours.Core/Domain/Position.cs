using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain {

    public class Position : ValueObject {
        public double Lat { get; set; }
        public double Long { get; set; }

        private Position() { }

        [JsonConstructor]
        public Position(double lat, double lon) {
            Lat = lat;
            Long = lon;
        }

        protected override IEnumerable<object> GetEqualityComponents() {
            yield return Lat;
            yield return Long;
        }
    }
}
