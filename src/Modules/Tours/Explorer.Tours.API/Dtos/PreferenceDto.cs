using Explorer.BuildingBlocks.Core.Domain;
using System.Collections.Generic;

namespace Explorer.Tours.API.Dtos
{
    public class TransportRatingDto
    {
        public TransportMode Mode { get; set; }
        public int Rating { get; set; }
    }

    public class PreferenceDto
    {
        public TourDifficulty PreferredDifficulty { get; set; }
        public List<TransportRatingDto> TransportRatings { get; set; } // Ažurirano da koristi listu TransportRatingDto
        public List<string> InterestTags { get; set; }

        public PreferenceDto()
        {
            TransportRatings = new List<TransportRatingDto>(); // Inicijalizacija liste
            InterestTags = new List<string>(); // Inicijalizacija liste
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
