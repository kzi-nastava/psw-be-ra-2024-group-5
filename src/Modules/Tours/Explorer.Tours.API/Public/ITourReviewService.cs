using Explorer.BuildingBlocks.Core.UseCases;
using System.Collections.Generic;
using FluentResults;
using Explorer.Tours.API.Dtos.TourLifecycle;

namespace Explorer.Tours.API.Public
{
    public interface ITourReviewService
    {
        Result<TourReviewDto> Create(TourReviewDto review);
        Result<TourReviewDto> GetById(int id);
        Result<List<TourReviewDto>> GetByTourId(int tourId);
        Result<List<TourReviewDto>> GetByTouristId(int touristId);
        Result<TourReviewDto> Update(TourReviewDto review);
        Result Delete(int id);
    }
}