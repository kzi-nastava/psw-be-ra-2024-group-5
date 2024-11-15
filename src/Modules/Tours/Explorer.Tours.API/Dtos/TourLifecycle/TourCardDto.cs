using Explorer.Tours.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourLifecycle
{
    public class TourCardDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Tags { get; set; }
        public TourLevel? Level { get; set; }
        public TourStatus Status { get; set; }
        public MoneyDto Price { get; set; }
        public double? Length { get; set; }
        public long AuthorId { get; set; }
        public KeyPointDto FirstKeypoint { get; set; }
        public DateTime PublishedTime { get; set; }
        public double? AverageRating { get; set; }
        public TourCardDto() { }
        public TourCardDto(long id, string? name, string? tags, TourLevel? level, TourStatus status, MoneyDto price,
            long authorId, double? length, DateTime publishedTime, KeyPointDto firstKeypoint, double? averageRating)
        {
            Id = id;
            Name = name;
            Tags = tags;
            Level = level;
            Status = status;
            Price = price;
            Length = length;
            AuthorId = authorId;
            FirstKeypoint = firstKeypoint;
            PublishedTime = publishedTime;
            AverageRating = averageRating;
          
        }

        public static double? CalculateAverageRating(List<TourReviewDto> reviews)
        {
            if (reviews == null || reviews.Count == 0)
            {
                return null;  
            }

            double totalRating = reviews.Sum(r => r.Rating);  
            return totalRating / reviews.Count;  
        }
    }
}
