using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System.Collections.Generic;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
    public interface ITourReviewRepository : ICrudRepository<TourReview>
    {
        Result<List<TourReview>> GetByTourId(int tourId);
        Result<List<TourReview>> GetByTouristId(int touristId);
    }
}