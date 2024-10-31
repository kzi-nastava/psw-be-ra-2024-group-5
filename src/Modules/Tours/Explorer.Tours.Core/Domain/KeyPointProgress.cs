using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain {
    public class KeyPointProgress : Entity {
        public long KeyPointId { get; private set; }
        public KeyPoint KeyPoint { get; init; }
        public DateTime? VisitTime { get; private set; } = null;
        public KeyPointProgress() { }
        public KeyPointProgress(KeyPoint keyPoint) { 
            KeyPoint = keyPoint;
            VisitTime = DateTime.UtcNow;
        }
    }
}
