using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Author
{
    public class FacilityService : BaseService<FacilityDto, Facility>, IFacilityService
    {
        private readonly ICrudRepository<Facility> _facilityReposistory;
        public FacilityService(ICrudRepository<Facility> facilityRepository, IMapper mapper) : base(mapper)
        {
            _facilityReposistory = facilityRepository;
        }
        public Result<FacilityDto> Create(FacilityDto facilityDto)
        {
            try {
                var facility = MapToDomain(facilityDto);

                facility = _facilityReposistory.Create(facility);

                return MapToDto(facility);
            }
            catch(Exception ex)
            {
                return Result.Fail("Failed to create facility: " + ex.Message);
            }
        }
    }
}
