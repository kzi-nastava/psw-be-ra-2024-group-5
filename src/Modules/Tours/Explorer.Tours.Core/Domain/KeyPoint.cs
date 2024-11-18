using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public class KeyPoint : Entity {
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public byte[] Image { get; private set; }
    public long TourId { get; private set; }
    public KeyPoint() { }

    public KeyPoint(string name, string? description, double latitude, double longitude, byte[] image, long tourId) {
        if (string.IsNullOrWhiteSpace(name)) 
            throw new ArgumentException("Invalid Name.");
        Latitude = latitude;
        Longitude = longitude;
        Name = name;
        Description = description;
        Image = image;
        TourId = tourId;
    }

    public KeyPoint(long id, string name, string? description, double latitude, double longitude, byte[] image, long tourId) {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Invalid Name.");
        Id = id;
        Latitude = latitude;
        Longitude = longitude;
        Name = name;
        Description = description;
        Image = image;
        TourId = tourId;
    }

}