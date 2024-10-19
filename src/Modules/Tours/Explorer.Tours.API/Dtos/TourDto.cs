using Explorer.Tours.API.Enum;

namespace Explorer.Tours.API.Dtos;

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
