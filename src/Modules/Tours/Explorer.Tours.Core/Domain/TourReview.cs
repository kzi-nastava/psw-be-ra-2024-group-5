using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;

namespace Explorer.Tours.Core.Domain
{
    public class TourReview : Entity
    {
        public int Rating { get; private set; }
        public string Comment { get; private set; }
        public DateTime VisitDate { get; private set; }
        public DateTime ReviewDate { get; private set; }
        public byte[]? Image { get; private set; }
        public long TourId { get; private set; }
        public long TouristId { get; private set; }
        public double CompletionPercentage { get; set; }
        public TourReview(int rating, string comment, DateTime visitDate,DateTime reviewDate, long tourId, long touristId, byte[]? image = null, double completionPercentage = 0)
        {
            ValidateRating(rating);
            ValidateComment(comment);
            ValidateVisitDate(visitDate);

            Rating = rating;
            Comment = comment;
            VisitDate = visitDate;
            ReviewDate = reviewDate;
            TourId = tourId;
            TouristId = touristId;
            Image = image;
            CompletionPercentage = completionPercentage;
        }

        public TourReview(long id,int rating, string comment, DateTime visitDate, DateTime reviewDate, long tourId, long touristId, byte[]? image = null, double completionPercentage = 0)
        {
            ValidateRating(rating);
            ValidateComment(comment);
            ValidateVisitDate(visitDate);

            Id = id;
            Rating = rating;
            Comment = comment;
            VisitDate = visitDate;
            ReviewDate = reviewDate;
            TourId = tourId;
            TouristId = touristId;
            Image = image;
            CompletionPercentage = completionPercentage;
        }

        private void ValidateRating(int rating)
        {
            if (rating < 1 || rating > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5.");
            }
        }

        private void ValidateComment(string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
            {
                throw new ArgumentNullException(nameof(comment), "Comment cannot be null or empty.");
            }
        }

        private void ValidateVisitDate(DateTime visitDate)
        {
            if (visitDate > DateTime.UtcNow)
            {
                throw new ArgumentException("Visit date cannot be in the future.", nameof(visitDate));
            }
        }

    }
}