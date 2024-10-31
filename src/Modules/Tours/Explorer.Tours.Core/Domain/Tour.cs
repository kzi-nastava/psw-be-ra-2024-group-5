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
    public List<KeyPoint>? KeyPoints { get; private set; }
    public List<TourReview>? Reviews { get; private set; }
    public double? Length {  get; private set; }
    public TourTransport? Transport { get; private set; }
    public double? Duration { get; private set; }
    public DateTime? PublishedTime { get; private set; }
    public DateTime? ArchivedTime { get; private set; }


    public Tour() { }

    public Tour(long id, string? name, string? description, string? tags, TourLevel? level, TourStatus status, Money price, long authorId, List<KeyPoint>? keyPoints, List<TourReview>? reviews, double? length, TourTransport? transport, double? duration, DateTime? publishedTime, DateTime? archivedTime) {
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
        Transport = transport;
        Duration = duration;
        PublishedTime = publishedTime;
        ArchivedTime = archivedTime;
    }

    public Tour(string? name, string? description, TourLevel? level, string? tags, long authorId, List<KeyPoint> keyPoints, List<TourReview> reviews, double? length, TourTransport? tourTransport, double? duration) {
        //if(string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Invalid name.");
        //if(string.IsNullOrWhiteSpace(description)) throw new ArgumentNullException("Invalid description.");
        //if(string.IsNullOrWhiteSpace(tags)) throw new ArgumentNullException("Invalid tags.");
        //if(level == null) throw new ArgumentNullException("Invalid level");
        //if(long.IsNegative(authorId)) throw new ArgumentNullException("Invalid author id");
        Name = name;
        Description = description;
        Tags = tags;
        Level = level;
        Status = TourStatus.Draft;
        Price = new Money(0.0, Currency.Rsd);
        AuthorId = authorId;
        KeyPoints = keyPoints;
        Reviews = reviews;
        Length = length;
        Transport = tourTransport;
        Duration = duration;
        PublishedTime = DateTime.UtcNow;
        ArchivedTime = DateTime.MinValue; // ili null da bude

    }
    public List<KeyPoint> AddKeyPoint(KeyPoint keyPoint) {
        KeyPoints.Add(keyPoint);
        return KeyPoints;
    }
    
}