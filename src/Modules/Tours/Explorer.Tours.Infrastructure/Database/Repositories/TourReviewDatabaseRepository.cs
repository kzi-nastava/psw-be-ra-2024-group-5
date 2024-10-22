using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourReviewDatabaseRepository : ITourReviewRepository
    {
        private readonly ToursContext _dbContext;

        public TourReviewDatabaseRepository(ToursContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TourReview Create(TourReview entity)
        {
            _dbContext.TourReviews.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public TourReview Update(TourReview entity)
        {
            try
            {
                _dbContext.TourReviews.Update(entity);
                _dbContext.SaveChanges();
                return entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new KeyNotFoundException("Tour review not found.");
            }
        }

        public void Delete(long id)
        {
            var tourReview = Get(id);
            _dbContext.TourReviews.Remove(tourReview);
            _dbContext.SaveChanges();
        }

        public TourReview Get(long id)
        {
            var tourReview = _dbContext.TourReviews.Find(id);
            if (tourReview == null) throw new KeyNotFoundException("Tour review not found.");
            return tourReview;
        }

        public PagedResult<TourReview> GetPaged(int page, int pageSize)
        {
            var task = _dbContext.TourReviews
                .GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public Result<List<TourReview>> GetByTourId(int tourId)
        {
            try
            {
                var reviews = _dbContext.TourReviews
                    .Where(r => r.TourId == tourId)
                    .ToList();
                return Result.Ok(reviews);
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error(ex.Message));
            }
        }

        public Result<List<TourReview>> GetByTouristId(int touristId)
        {
            try
            {
                var reviews = _dbContext.TourReviews
                    .Where(r => r.TouristId == touristId)
                    .ToList();
                return Result.Ok(reviews);
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error(ex.Message));
            }
        }
    }
}