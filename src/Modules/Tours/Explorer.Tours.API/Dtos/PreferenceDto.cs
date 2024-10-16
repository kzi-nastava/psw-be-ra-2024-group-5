using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class PreferenceDto
    {
        public TourDifficulty PreferredDifficulty { get; set; }
        public Dictionary<TransportMode, int> TransportRatings { get; set; }
        public List<string> InterestTags { get; set; }

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

