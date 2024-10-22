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
        private readonly ICrudRepository<Facility> _facilityRepository;
        public FacilityService(ICrudRepository<Facility> facilityRepository, IMapper mapper) : base(mapper)
        {
            _facilityRepository = facilityRepository;
        }
        public Result<FacilityDto> Create(FacilityDto facilityDto)
        {
            try {
                var facility = MapToDomain(facilityDto);

                facility = _facilityRepository.Create(facility);

                return MapToDto(facility);
            }
            catch(Exception ex)
            {
                return Result.Fail("Failed to create facility: " + ex.Message);
            }
        }

        public Result<List<FacilityDto>> GetPaged(int page, int pageSize)
        {
            var facilities = _facilityRepository.GetPaged(page, pageSize);

            var facilityDtos = new List<FacilityDto>();

            foreach (var facility in facilities.Results)
            {
                facilityDtos.Add(MapToDto(facility));
            }

            return facilityDtos;
        }

        public Result<FacilityDto> Update(FacilityDto facilityDto)
        {
            try
            {
                var facility = MapToDomain(facilityDto);

                _facilityRepository.Update(facility);

                return MapToDto(facility);
            }
            catch (Exception ex)
            {
                return Result.Fail("Failed to update existing facility: " + ex.Message);
            }
        }

        public Result<FacilityDto> Get(long id)
        {
            try
            {
                var facility = _facilityRepository.Get(id);

                return MapToDto(facility);
            }
            catch (Exception ex)
            {
                return Result.Fail("Failed to get existing facility: " + ex.Message);
            }
        }
    }
}
