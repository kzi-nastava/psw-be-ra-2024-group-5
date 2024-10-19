public class KeyPointDto {
    public int Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string Image { get; set; } 
    public long TourId { get; set; }
}
