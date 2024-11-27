public class KeyPointDto {

    public long Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; } 
    public long TourId { get; set; } // SHUOLD BE REMOVED
    public string? Secret { get; set; }
    public string? SecretImage { get; set; }
    public KeyPointDto() { }

    public KeyPointDto(long id, double latitude, double longitude, string name, string? description, long tourId, string? secret = null)
    {
        Id = id;
        Latitude = latitude;
        Longitude = longitude;
        Name = name;
        Description = description;
        TourId = tourId;
        Secret = secret;
    }

}
