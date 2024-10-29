using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain {
    public class TourExecution : Entity {

        public long UserId { get; init; }
        public Position LastUserPosition { get; set; }
        public TourExecutionStatus Status { get; private set; }
        public DateTime? SessionEnd { get; set; } = null;
        public Tour Tour { get; init; }

        private TourExecution() { }

        public TourExecution(long userId, Position lastUserPosition, Tour tour) {
            UserId = userId;
            LastUserPosition = lastUserPosition;
            Status = TourExecutionStatus.Active;
            Tour = tour;
        }
    }
}
