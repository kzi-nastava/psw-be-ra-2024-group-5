using Explorer.Tours.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourLifecycle;
public class TourCreationDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Tags { get; set; }
    public TourLevel? Level { get; set; }
    public long AuthorId { get; set; }
    public List<KeyPointDto> KeyPoints { get; set; }
    public double? Length { get; set; }
    public List<TransportDurationDto> TransportDurations { get; set; }

    public TourCreationDto() { }
    public TourCreationDto(string? name, string? description,
        string? tags, TourLevel? level, long authorId, List<KeyPointDto> keyPoints,
        double? length, List<TransportDurationDto> transportDurations)
    {
        Name = name;
        Description = description;
        Tags = tags;
        Level = level;
        AuthorId = authorId;
        KeyPoints = keyPoints;
        Length = length;
        TransportDurations = transportDurations;
    }
}
