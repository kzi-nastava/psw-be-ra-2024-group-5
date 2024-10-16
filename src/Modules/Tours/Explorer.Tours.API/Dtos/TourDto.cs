namespace Explorer.Tours.API.Dtos;
public enum TourLevel { Beginner, Intermediate, Advanced }
public enum TourStatus { Draft, Active, Finished, Canceled = -1 }
public class TourDto {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Tags { get; set; }   
    public TourLevel? Level { get; set; }
    public TourStatus Status { get; set; }
    public double Price { get; set; }
    public long AuthorId { get; set; }
}
