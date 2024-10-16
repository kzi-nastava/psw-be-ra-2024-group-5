using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Preference : Entity
    {
        public TourDifficulty PreferredDifficulty { get; init; }
        public Dictionary<TransportMode, int> TransportRatings { get; init; }
        public List<string> InterestTags { get; init; }

        public Preference(TourDifficulty preferredDifficulty, Dictionary<TransportMode, int> transportRatings, List<string> interestTags)
        {

            if (!Enum.IsDefined(typeof(TourDifficulty), preferredDifficulty))
            {
                throw new ArgumentException("Invalid tour difficulty. Allowed values are BEGINNER, INTERMEDIATE, and ADVANCED.");
            }

            foreach (var rating in transportRatings)
            {
                if (!Enum.IsDefined(typeof(TransportMode), rating.Key))
                {
                    throw new ArgumentException("Invalid transport mode.");
                }

                if (rating.Value < 0 || rating.Value > 3)
                {
                    throw new ArgumentException("Transport rating must be between 0 and 3.");
                }
            }

            PreferredDifficulty = preferredDifficulty;
            TransportRatings = transportRatings ?? new Dictionary<TransportMode, int>();
            InterestTags = interestTags ?? new List<string>();
        }

        //Timska konvencija za tezinu ture
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
}