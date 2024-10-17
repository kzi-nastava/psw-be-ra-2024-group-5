using Explorer.BuildingBlocks.Core.Domain;
using System.Collections.Generic;

namespace Explorer.Tours.Core.Domain
{
    public class TransportRating : Entity
    {
        public TransportMode Mode { get; set; }
        public int Rating { get; set; }
    }

    public class Preference : Entity
    {
        public TourDifficulty PreferredDifficulty { get; private set; }
        public List<TransportRating> TransportRatings { get; private set; } // Sada lista transportnih ocena
        public List<string> InterestTags { get; private set; }
        public Preference() { }

        public Preference(TourDifficulty preferredDifficulty, List<TransportRating> transportRatings, List<string> interestTags)
        {
            if (!Enum.IsDefined(typeof(TourDifficulty), preferredDifficulty))
            {
                throw new ArgumentException("Invalid tour difficulty. Allowed values are BEGINNER, INTERMEDIATE, and ADVANCED.");
            }

            foreach (var rating in transportRatings)
            {
                if (!Enum.IsDefined(typeof(TransportMode), rating.Mode))
                {
                    throw new ArgumentException("Invalid transport mode.");
                }

                if (rating.Rating < 0 || rating.Rating > 3)
                {
                    throw new ArgumentException("Transport rating must be between 0 and 3.");
                }
            }

            PreferredDifficulty = preferredDifficulty;
            TransportRatings = transportRatings ?? new List<TransportRating>();
            InterestTags = interestTags ?? new List<string>();
        }
    }

    public enum TourDifficulty
    {
        BEGINNER,
        INTERMEDIATE,
        ADVANCED
    }

    public enum TransportMode
    {
        WALKING,
        CYCLING,
        CAR,
        BOAT
    }
}
