using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Enum;

namespace Explorer.Tours.Core.Domain
{
    public class Facility: Entity
    {
        public string Name { get; init; }
        public string? Description { get; init; }
        public FacilityType Type { get; init; }
        public byte[]? Image { get; init; }
        public double Longitude { get; init; }
        public double Latitude { get; init; }   
        public Facility() { }
        public Facility(string name, string? description, double longitude, double latitude, byte[]? image, FacilityType type = FacilityType.Other)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid Name.");
            Name = name;
            Description = description;
            Longitude = longitude;
            Latitude = latitude;
            Type = type;
            Image = image;
        }
        public Facility(long id, string name, string? description, FacilityType type, byte[]? image, double longitude, double latitude) {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid Name.");
            Id = id;
            Name = name;
            Description = description;
            Type = type;
            Image = image;
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}
