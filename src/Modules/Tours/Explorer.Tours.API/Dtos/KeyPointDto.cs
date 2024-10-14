using Microsoft.AspNetCore.Http;

namespace Explorer.Tours.API.Dtos;

public class KeyPointDto {
    public int Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public IFormFile Image { get; set; }
}