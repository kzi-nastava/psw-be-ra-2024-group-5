using Explorer.BuildingBlocks.Core.Domain;
using System.Collections.Generic;

namespace Explorer.Tours.Core.Domain
{

    public class Preference : Entity
    {
        public long TouristId { get; private set; }
        public TourDifficulty PreferredDifficulty { get; private set; }
        public int WalkRating { get; private set; }
        public int BikeRating { get; private set; }
        public int CarRating { get; private set; }
        public int BoatRating { get; private set; }
        public List<string> InterestTags { get; private set; }

        public Preference(long touristId, TourDifficulty preferredDifficulty, int walkRating, int bikeRating, int carRating, int boatRating, List<string> interestTags)
        {

            if (!Enum.IsDefined(typeof(TourDifficulty), preferredDifficulty))
            {
                throw new ArgumentException("Invalid tour difficulty. Allowed values are BEGINNER, INTERMEDIATE, and ADVANCED.");
            }

            if (walkRating < 0 || walkRating > 3)
            {
                throw new ArgumentException("Walk rating must be between 0 and 3.");
            }

            if (bikeRating < 0 || bikeRating > 3)
            {
                throw new ArgumentException("Bike rating must be between 0 and 3.");
            }

            if (carRating < 0 || carRating > 3)
            {
                throw new ArgumentException("Car rating must be between 0 and 3.");
            }

            if (boatRating < 0 || boatRating > 3)
            {
                throw new ArgumentException("Boat rating must be between 0 and 3.");
            }

           
            TouristId = touristId;
            PreferredDifficulty = preferredDifficulty;
            WalkRating = walkRating;
            BikeRating = bikeRating;
            CarRating = carRating;
            BoatRating = boatRating;
            InterestTags = interestTags ?? new List<string>(); // Postavlja praznu listu ako je null
        }


        public enum TourDifficulty
        {
            BEGINNER,
            INTERMEDIATE,
            ADVANCED
        }

    }
}
