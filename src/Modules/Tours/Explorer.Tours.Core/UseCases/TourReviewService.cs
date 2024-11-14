using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using Explorer.Tours.API.Dtos.TourLifecycle;

namespace Explorer.Tours.Core.UseCases
{
    public class TourReviewService : CrudService<TourReviewDto, TourReview>, ITourReviewService
    {
        private readonly ITourReviewRepository _repository;
        private readonly IUserRepository _userRepository;

        public TourReviewService(ITourReviewRepository repository, IUserRepository userRepository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public override Result<TourReviewDto> Create(TourReviewDto review)
        {
            try
            {
                review.ReviewDate = DateTime.UtcNow;
                var domainReview = MapToDomain(review);
                var createdReview = _repository.Create(domainReview);
                return MapToDto(createdReview);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public Result<TourReviewDto> GetById(int id)
        {
            return Get(id);
        }

        public Result<List<TourReviewDto>> GetByTourId(int tourId)
        {
            try
            {
                var reviews = _repository.GetByTourId(tourId);
                return MapToDto(reviews.Value);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.NotFound).WithError(ex.Message);
            }
        }

        public Result<List<TourReviewDto>> GetByTouristId(int touristId)
        {
            try
            {
                var reviews = _repository.GetByTouristId(touristId);
                return MapToDto(reviews.Value);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.NotFound).WithError(ex.Message);
            }
        }

        public override Result<TourReviewDto> Update(TourReviewDto review)
        {
            try
            {
                var domainReview = MapToDomain(review);
                var updatedReview = _repository.Update(domainReview);
                return MapToDto(updatedReview);
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(ex.Message);
            }
        }

        public override Result Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail(FailureCode.NotFound).WithError(ex.Message);
            }
        }
    }
}