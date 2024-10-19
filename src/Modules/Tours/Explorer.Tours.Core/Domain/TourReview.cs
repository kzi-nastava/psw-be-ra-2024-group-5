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
        public int TourId { get; private set; }
        public int TouristId { get; private set; }

        public TourReview(int rating, string comment, DateTime visitDate,DateTime reviewDate, int tourId, int touristId, byte[]? image = null)
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