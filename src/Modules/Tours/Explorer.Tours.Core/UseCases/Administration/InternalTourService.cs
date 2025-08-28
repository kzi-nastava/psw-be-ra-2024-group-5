using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Internal;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos.TourLifecycle;
using Explorer.Tours.API.Internal;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class InternalTourService: IInternalTourService
    {
        private readonly ITourRepository _tourRepository;

        protected readonly IMapper _mapper;
        public InternalTourService(ITourRepository repository, IMapper mapper) { 
            _tourRepository = repository;
            _mapper = mapper;
        }
        public Result<TourDto> GetById(long id)
        {
            try
            {
                var tour = _tourRepository.GetById(id);
                var tourDto = _mapper.Map<TourDto>(tour);
                return Result.Ok(tourDto);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<List<TourDto>> GetByAuthorPaged(int authorId, int page, int pageSize)
        {
            try
            {
                var toursByAuthor = _tourRepository.GetByAuthorPaged(authorId, page, pageSize);
                var tourDto = _mapper.Map<List<TourDto>>(toursByAuthor);
                return Result.Ok(tourDto);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<List<TourCardDto>> GetTourCardsExceptIds(List<long> tourIds)
        {
            try
            {
                var tours = _tourRepository.GetToursWithoutIds(tourIds);
                var tourDtos = _mapper.Map<List<TourCardDto>>(tours);
                return Result.Ok(tourDtos);
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

    }
}
