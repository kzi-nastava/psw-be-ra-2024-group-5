using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourExecution;
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

        public Result<TourExecutionDto> GetActive(long userId) {
            try {
                var activeTour = _tourExecutionRepository.GetActive(userId);

                var tourExecutionDto = _mapper.Map<TourExecutionDto>(activeTour);
                return Result.Ok(tourExecutionDto);
            }
            catch (Exception e) {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
        }

        public Result<TourExecutionDto> StartTourExecution(TourExecutionStartDto tourExecutionStart) {
            try {
                var tourExecution = new TourExecution(
                    tourExecutionStart.UserId,
                    tourExecutionStart.TourId);

                var createdTourExecution = _tourExecutionRepository.Create(tourExecution);

                var tourExecutionDto = _mapper.Map<TourExecutionDto>(createdTourExecution);

                return Result.Ok(tourExecutionDto);
            }
            catch (Exception e) {
                return Result.Fail<TourExecutionDto>(e.Message);
            }            
        }

        public Result<KeyPointProgressDto> Progress(long tourExecutionId, PositionDto newPositionDto) {
            try {
                var newPosition = new Position(newPositionDto.Latitude, newPositionDto.Longitude);

                var currentSession = _tourExecutionRepository.Get(tourExecutionId);
                var tour = _tourRepository.GetById((int) currentSession.TourId);

                var completedKeyPoint = currentSession.Progress(newPosition, tour.KeyPoints);

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
