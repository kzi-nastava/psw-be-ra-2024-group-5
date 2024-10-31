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
            SessionEnd = DateTime.UtcNow;
        }

        public void Abandon() {
            Status = TourExecutionStatus.Abandoned;
            SessionEnd = DateTime.UtcNow;
        }

        public KeyPointProgress? Progress(Position newPosition) {
            LastActivity = DateTime.UtcNow;

            return CheckKeyPointReached(newPosition);
        }

        List<KeyPoint> keyPoints = new List<KeyPoint> {
            new KeyPoint(1, "a", "a", 0, 0, new byte[1], 1)
        };
        private KeyPointProgress? CheckKeyPointReached(Position newPosition) {
            foreach (var keyPoint in GetNonCompleted()) {
                if (GeoCalculator.IsClose(newPosition, new Position(keyPoint.Latitude, keyPoint.Longitude), 15)) {
                    var newProgress = new KeyPointProgress(keyPoint);
                    KeyPointProgresses.Add(newProgress);

                    if(GetNonCompleted().Count() == 0)
                        Complete();

                    return newProgress;
                }
            }

            return null;
        }

        private IEnumerable<KeyPoint> GetNonCompleted() {
            return keyPoints.Where(kp => !KeyPointProgresses.Any(kpp => kpp.KeyPoint.Id == kp.Id));
        }
    }
}
