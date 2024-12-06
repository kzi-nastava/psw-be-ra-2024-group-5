using Explorer.Tours.API.Enum;

namespace Explorer.Tours.API.Dtos.TourLifecycle;

public class TourDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Tags { get; set; }
    public TourLevel? Level { get; set; }
    public TourStatus Status { get; set; }
    public MoneyDto Price { get; set; }
    public long AuthorId { get; set; }
    public List<KeyPointDto> KeyPoints { get; set; }
    public List<TourReviewDto> Reviews { get; set; }
    public double? Length { get; set; }
    public List<TransportDurationDto> TransportDurations { get; set; }
    public DateTime PublishedTime { get; set; }
    public DateTime ArchivedTime { get; set; }

    

    public TourDto() { }
    public TourDto(long id, string? name, string? description,
        string? tags, TourLevel? level, TourStatus status, MoneyDto price,
        long authorId, List<KeyPointDto> keyPoints, List<TourReviewDto> reviews,
        double? length, List<TransportDurationDto> transportDurations,
        DateTime publishedTime, DateTime archivedTime)
    {
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

}
