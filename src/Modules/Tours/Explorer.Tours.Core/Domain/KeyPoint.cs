using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public class KeyPoint : Entity {
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public byte[]? Image { get; init; }
    public int TourId { get; init; }

    public KeyPoint(string name, string? description, double latitude, double longitude, byte[]? image, int tourId) {
        if (string.IsNullOrWhiteSpace(name)) 
            throw new ArgumentException("Invalid Name.");
        Latitude = latitude;
        Longitude = longitude;
        Name = name;
        Description = description;
        Image = image;
        TourId = tourId;
    }
}