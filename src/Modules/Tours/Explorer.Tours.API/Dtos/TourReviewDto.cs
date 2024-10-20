using System;
using System.Collections.Generic;

namespace Explorer.Tours.API.Dtos
{
    public class TourReviewDto
    {
        public long Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime ReviewDate { get; set; }
        public string? Image { get; set; }
        public long TourId { get; set; }
        public long TouristId { get; set; }
    }
}