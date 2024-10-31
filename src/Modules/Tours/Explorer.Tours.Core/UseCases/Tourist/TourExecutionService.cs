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

        private readonly ITourRepository _tourRepository;
        private readonly ITourExecutionRepository _tourExecutionRepository;

        public TourExecutionService(ITourRepository tourRepository, ITourExecutionRepository tourExecutionRepository) {
            _tourRepository = tourRepository;
            _tourExecutionRepository = tourExecutionRepository;
        }

        public Result<bool> StartTourExecution(long tourId, TourExecutionDto tourExecutionDto) {
            try {
                var tour = _tourRepository.Get(tourId);

                var tourExecution = new TourExecution(
                    tourExecutionDto.UserId,
                    new Position(tourExecutionDto.Latitude, tourExecutionDto.Longitude),
                    tour);

                _tourExecutionRepository.Create(tourExecution);
            }
            catch (Exception e) {
                return Result.Fail<bool>(e.Message);
            }

            return Result.Ok(true);
        }
    }
}
