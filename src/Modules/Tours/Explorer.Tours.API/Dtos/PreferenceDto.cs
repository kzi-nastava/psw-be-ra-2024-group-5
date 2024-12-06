using Explorer.BuildingBlocks.Core.Domain;
using System.Collections.Generic;
using Explorer.Tours.API.Enum;

namespace Explorer.Tours.API.Dtos
{
   
    public class PreferenceDto
    {
        public long Id { get; set; }
        public int TouristId { get; set; }
        public TourLevel PreferredDifficulty { get; set; }
        public int WalkRating { get; set; }
        public int BikeRating { get; set; }
        public int CarRating { get; set; }
        public int BoatRating { get; set; }
        public List<string> InterestTags { get; set; }
        public bool isActive { get; set; }


    }

    public enum TourDifficulty
    {
        BEGINNER,
        INTERMEDIATE,
        ADVANCED
    }

}
