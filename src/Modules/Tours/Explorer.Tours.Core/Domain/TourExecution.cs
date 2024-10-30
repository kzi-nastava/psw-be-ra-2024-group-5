using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Enum;
using Explorer.Tours.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain {
    public class TourExecution : Entity {

        public long UserId { get; init; }
        public Position LastUserPosition { get; private set; }
        public TourExecutionStatus Status { get; private set; }
        public DateTime? SessionEnd { get; private set; } = null;
        public DateTime? LastActivity { get; private set; } = null;
        public Tour Tour { get; init; }

        public ICollection<KeyPointProgress> KeyPointProgresses { get; set; } = new List<KeyPointProgress>();

        private TourExecution() { }

        public TourExecution(long userId, Position lastUserPosition, Tour tour) {
            UserId = userId;
            LastUserPosition = lastUserPosition;
            Status = TourExecutionStatus.Active;
            Tour = tour;
        }

        private void Complete() {
            Status = TourExecutionStatus.Completed;
            SessionEnd = DateTime.Now;
        }

        public void Abandon() {
            Status = TourExecutionStatus.Abandoned;
            SessionEnd = DateTime.Now;
        }

        public KeyPoint? Progress(Position newPosition) {
            ChangePosition(newPosition);

            foreach (var kp in KeyPointProgresses.Where(kp => kp.VisitTime == null)) {
                if(GeoCalculator.IsClose(LastUserPosition, new Position(kp.KeyPoint.Latitude, kp.KeyPoint.Longitude), 15)){
                    kp.ConfirmVisit();

                    if(AreAllKeyPointsVisited())
                        Complete();

                    return kp.KeyPoint;
                }
            }

            return null;
        }

        private void ChangePosition(Position newPosition) {
            LastActivity = DateTime.Now;
            LastUserPosition = newPosition;
        }

        private bool AreAllKeyPointsVisited() {
            return !KeyPointProgresses.Any(kp => kp.VisitTime == null);
        }
    }
}
