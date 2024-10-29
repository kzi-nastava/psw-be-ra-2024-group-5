using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Tourist {
    public class TourExecutionService {

        private readonly ITourRepository _tourRepository;

        public TourExecutionService(ITourRepository tourRepository) {
            _tourRepository = tourRepository;
        }

        public Result<bool> StartTourExecution(TourExecutionDto tourExecutionDto) {
            try {
                var tour = _tourRepository.Get(tourExecutionDto.TourId);

                var tourExecution = new TourExecution(
                    tourExecutionDto.UserId,
                    new Position(tourExecutionDto.Latitude, tourExecutionDto.Longitude),
                    tour);

            }
            catch (Exception e) {
                return Result.Fail<bool>(e.Message);
            }

            return Result.Ok(true);
        }
    }
}
