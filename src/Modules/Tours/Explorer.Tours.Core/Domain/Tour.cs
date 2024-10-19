using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Enum;

namespace Explorer.Tours.Core.Domain;

public class Tour : Entity {
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public string? Tags { get; private set; }
    public TourLevel? Level { get; private set; }
    public TourStatus Status { get; private set; }
    public double Price { get; private set; }
    
    public long AuthorId { get; private set; }

    public Tour(string? name, string? description,  TourLevel? level, string? tags, long authorId) {
        if(string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Invalid name.");
        if(string.IsNullOrWhiteSpace(description)) throw new ArgumentNullException("Invalid description.");
        if(string.IsNullOrWhiteSpace(tags)) throw new ArgumentNullException("Invalid tags.");
        if(level == null) throw new ArgumentNullException("Invalid level");
        if(long.IsNegative(authorId)) throw new ArgumentNullException("Invalid author id");
        Name = name;
        Description = description;
        Tags = tags;
        Level = level;
        Status = TourStatus.Draft;
        Price = 0.0;
        AuthorId = authorId;
    }
    
}