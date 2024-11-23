using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Enum;

namespace Explorer.Tours.Core.Domain;

public class Tour : Entity {
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public string? Tags { get; private set; }
    public TourLevel? Level { get; private set; }
    public TourStatus Status { get; private set; }
    public Money Price { get; private set; }
    public long AuthorId { get; private set; }
    public List<KeyPoint> KeyPoints { get; private set; }
    public List<TourReview> Reviews { get; private set; }
    public double? Length {  get; private set; }
    public List<TransportDuration> TransportDurations { get; private set; }
    public DateTime PublishedTime { get; private set; }
    public DateTime ArchivedTime { get; private set; }

    public Tour() { }

    //Constructor with all parametars
    public Tour(long id, string? name, string? description, string? tags,
        TourLevel? level, TourStatus status, Money price, long authorId,
        List<KeyPoint> keyPoints, List<TourReview> reviews, double? length,
        List<TransportDuration> transportDurations, DateTime publishedTime, DateTime archivedTime) {
        Id = id;
        Name = name;
        Description = description;
        Tags = tags;
        Level = level;
        Status = status;
        Price = price;
        AuthorId = authorId;
        KeyPoints = keyPoints;
        Reviews = reviews;
        Length = length;
        TransportDurations = transportDurations;
        PublishedTime = publishedTime;
        ArchivedTime = archivedTime;
    }

    //Constructor for creation
    public Tour(string? name, string? description, string? tags, TourLevel? level,
        long authorId, List<KeyPoint> keyPoints, double? length, List<TransportDuration> transportDurations) {
        Name = name;
        Description = description;
        Tags = tags;
        Level = level;
        Status = TourStatus.Draft;
        Price = new Money(0.0, Currency.Rsd);
        AuthorId = authorId;
        KeyPoints = keyPoints;
        Reviews = new List<TourReview>();
        Length = length;
        TransportDurations = transportDurations;
        PublishedTime = DateTime.MinValue;
        ArchivedTime = DateTime.MinValue; // ili null da bude

    }


    public static List<Tour> FilterToursByLocation(List<Tour> tours, double startLat, double endLat, double startLong, double endLong)
    {
        var filteredTours = new List<Tour>();

        foreach (var tour in tours)
        {
            bool tourFound = false;
            foreach (var keypoint in tour.KeyPoints)
            {
                if ((startLat < keypoint.Latitude && endLat > keypoint.Latitude) &&
                    (startLong < keypoint.Longitude && endLong > keypoint.Longitude))
                {
                    filteredTours.Add(tour);
                    tourFound = true;
                    break;
                }
            }
        }

        return filteredTours;
    }


    public void AddReview(TourReview review)
    {
        ValidateReviewAddition(review);
        Reviews.Add(review);
    }

    private void ValidateReviewAddition(TourReview review)
    {
        if (review == null) throw new ArgumentNullException(nameof(review));
        if (Status != TourStatus.Published)
            throw new InvalidOperationException("Cannot review an unpublished tour");
        if (Reviews.Any(r => r.TouristId == review.TouristId))
            throw new InvalidOperationException("Tourist has already reviewed this tour");
    }

    public List<TourReview> GetReviews()
    {
        return Reviews.ToList();
    }

    public List<KeyPoint> AddKeyPoint(KeyPoint keyPoint) {
        KeyPoints.Add(keyPoint);
        return KeyPoints;
    }

    public bool Publish(double priceAmount, Currency currency)
    {
        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description)
            || KeyPoints == null || KeyPoints.Count < 2
            || TransportDurations == null || !TransportDurations.Any()
            || Status == TourStatus.Published
            || string.IsNullOrWhiteSpace(Tags) || Level == null
            || priceAmount <= 0 || currency == null)
        {
            return false;
        }

        Price = new Money(priceAmount, currency);
        Status = TourStatus.Published;
        PublishedTime = DateTime.UtcNow;
        return true;
    }


    public bool Archive()
    {
        if(Status == TourStatus.Draft || Status == TourStatus.Archived)
        {
            return false;
        }

        Status = TourStatus.Archived;
        ArchivedTime = DateTime.UtcNow;
        return true;
    }

    public KeyPoint GetFirstKeypoint() {
        return KeyPoints[0];
    }

    public double GetAverageRating() {
        if(Reviews.Count > 0) 
            return Reviews.Average(x => x.Rating);
        return 0;
    }

}