using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Internal;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos.TourLifecycle;
using Explorer.Tours.API.Internal;
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
    }
}
