using System;
using System.Collections.Generic;

namespace Explorer.Tours.API.Dtos
{
    public class TourReviewDto
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime ReviewDate { get; set; }
        public string? Image { get; set; }
        public int TourId { get; set; }
        public int TouristId { get; set; }
    }
}