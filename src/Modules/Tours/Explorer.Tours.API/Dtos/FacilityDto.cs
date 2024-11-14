using Explorer.Tours.API.Enum;
namespace Explorer.Tours.API.Dtos
{
    public class FacilityDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public FacilityType Type { get; set; }
        public string? Image { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public FacilityDto() { }
        public FacilityDto(long id, string name, string? description, FacilityType type, string? image, double longitude, double latitude) {
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
