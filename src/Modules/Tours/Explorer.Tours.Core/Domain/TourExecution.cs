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
        public TourExecutionStatus Status { get; private set; }
        public DateTime? SessionStart { get; private set; } = null;
        public DateTime? SessionEnd { get; private set; } = null;
        public DateTime? LastActivity { get; private set; } = null;
        public long TourId { get; init; }
        public ICollection<KeyPointProgress> KeyPointProgresses { get; set; } = new List<KeyPointProgress>();

        private TourExecution() { }

        public TourExecution(long userId, long tourId) {
            UserId = userId;
            Status = TourExecutionStatus.Active;
            TourId = tourId;
            SessionStart = DateTime.UtcNow;
        }

        private void Complete() {
            Status = TourExecutionStatus.Completed;
            SessionEnd = DateTime.UtcNow;
        }

        public void Abandon() {
            Status = TourExecutionStatus.Abandoned;
            SessionEnd = DateTime.UtcNow;
        }

        public bool IsCompleted() {
            return Status == TourExecutionStatus.Completed;
        }

        public TimeSpan? GetCompletionDuration()
        {
            if (SessionEnd == null || SessionStart == null || Status != TourExecutionStatus.Completed)
                return null;

            return SessionEnd.Value - SessionStart.Value;
        }

        public KeyPointProgress? Progress(Location newPosition, IEnumerable<KeyPoint> keyPoints) {
            LastActivity = DateTime.UtcNow;

            return CheckKeyPointReached(newPosition, keyPoints);
        }

        private KeyPointProgress? CheckKeyPointReached(Location newPosition, IEnumerable<KeyPoint> keyPoints) {
            foreach (var keyPoint in GetNonCompleted(keyPoints)) {
                if (GeoCalculator.IsClose(newPosition, new Location(keyPoint.Latitude, keyPoint.Longitude), 15)) {
                    var newProgress = new KeyPointProgress(keyPoint);
                    KeyPointProgresses.Add(newProgress);

                    if(GetNonCompleted(keyPoints).Count() == 0)
                        Complete();

                    return newProgress;
                }
            }

            return null;
        }

        private IEnumerable<KeyPoint> GetNonCompleted(IEnumerable<KeyPoint> allKeyPoints) {
            return allKeyPoints.Where(kp => !KeyPointProgresses.Any(kpp => kpp.KeyPoint.Id == kp.Id));
        }
    }
}
