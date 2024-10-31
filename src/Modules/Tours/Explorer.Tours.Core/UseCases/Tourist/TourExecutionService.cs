using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Tourist;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Tourist {
    public class TourExecutionService : ITourExecutionService {

        private readonly IMapper _mapper;

        private readonly ITourRepository _tourRepository;
        private readonly ITourExecutionRepository _tourExecutionRepository;
        private readonly ICrudRepository<KeyPoint> _keyPointRepository;

        public TourExecutionService(ITourRepository tourRepository, ITourExecutionRepository tourExecutionRepository, ICrudRepository<KeyPoint> keyPointRepository, IMapper mapper) {
            _tourRepository = tourRepository;
            _tourExecutionRepository = tourExecutionRepository;
            _keyPointRepository = keyPointRepository;   
            _mapper = mapper;
        }

        public Result<TourExecutionDto> StartTourExecution(TourExecutionDto tourExecutionDto) {
            try {
                var tourExecution = new TourExecution(
                    tourExecutionDto.UserId,
                    new Position(tourExecutionDto.Latitude, tourExecutionDto.Longitude),
                    tourExecutionDto.TourId);

                var createdTourExecution = _tourExecutionRepository.Create(tourExecution);
                tourExecutionDto.Id = createdTourExecution.Id;

                return Result.Ok(tourExecutionDto);
            }
            catch (Exception e) {
                return Result.Fail<TourExecutionDto>(e.Message);
            }            
        }

        public Result<KeyPointProgressDto> Progress(TourExecutionDto tourExecution) {
            try {
                var currentSession = _tourExecutionRepository.Get(tourExecution.Id);
                var tour = _tourRepository.GetById((int) currentSession.TourId);

                var completedKeyPoint = currentSession.Progress(new Position(tourExecution.Latitude, tourExecution.Longitude), 
                    tour.KeyPoints);

                _tourExecutionRepository.Update(currentSession);

                return _mapper.Map<KeyPointProgressDto>(completedKeyPoint);
            }
            catch (KeyNotFoundException e) {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentNullException e) {
                return Result.Fail(FailureCode.Internal).WithError(e.Message);
            }
        }
    }
}
