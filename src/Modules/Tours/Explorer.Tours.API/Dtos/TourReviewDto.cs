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
        public double CompletionPercentage { get; set; }
        

        public TourReviewDto() { }
        public TourReviewDto(long id, int rating, string comment, DateTime visitDate, DateTime reviewDate, string? image, long tourId, long touristId, double completionPercentage)
        {
            Id = id;
            Rating = rating;
            Comment = comment;
            VisitDate = visitDate;
            ReviewDate = reviewDate;
            Image = image;
            TourId = tourId;
            TouristId = touristId;
            CompletionPercentage = completionPercentage;
           

        }

       

    }
}