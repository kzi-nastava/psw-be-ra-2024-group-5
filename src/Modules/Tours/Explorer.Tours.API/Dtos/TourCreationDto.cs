using Explorer.Tours.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos;
public class TourCreationDto {
    //public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Tags { get; set; }
    public TourLevel? Level { get; set; }
    //public TourStatus Status { get; set; }
    //public MoneyDto Price { get; set; }
    public long AuthorId { get; set; }
    public List<KeyPointDto> KeyPoints { get; set; }
    //public List<TourReviewDto>? Reviews { get; set; }
    public double? Length { get; set; }
    public List<TransportDurationDto> TransportDurationDtos { get; set; }
    //public DateTime? PublishedTime { get; set; }
    //public DateTime? ArchivedTime { get; set; }

    public TourCreationDto() { }
    public TourCreationDto(string name, string description,
        string tags, TourLevel level, long authorId, List<KeyPointDto> keyPoints,
        double length, List<TransportDurationDto> transportDurationDtos) {
        Name = name;
        Description = description;
        Tags = tags;
        Level = level;
        //Status = status;
        //Price = price;
        AuthorId = authorId;
        KeyPoints = keyPoints;
        Length = length;
        TransportDurationDtos = transportDurationDtos;
    }
}
