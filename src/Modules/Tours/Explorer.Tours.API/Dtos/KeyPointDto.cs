public class KeyPointDto {
    //private long id;
    //private object value;

    //public KeyPointDto(long id, object value) {
    //    this.id = id;
    //    this.value = value;
    //}

    public long Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; } 
    public long TourId { get; set; }
    public KeyPointDto(long id, double latitude, double longitude, string name, string? description, string? image, long tourId) {
        Id = id;
        Latitude = latitude;
        Longitude = longitude;
        Name = name;
        Description = description;
        Image = image;
        TourId = tourId;
    }
}
