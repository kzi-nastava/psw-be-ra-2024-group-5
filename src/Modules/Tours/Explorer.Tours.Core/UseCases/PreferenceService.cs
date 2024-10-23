using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Explorer.Tours.Core.UseCases
{
    public class PreferenceService : CrudService<PreferenceDto, Preference>, IPreferenceService
    {
        private readonly IMapper _mapper;

        public PreferenceService(ICrudRepository<Preference> repository, IMapper mapper) : base(repository, mapper) {

            _mapper = mapper;
        }

        public Result<List<PreferenceDto>> GetAll()
        {
            // Ako želite sve rezultate, možete koristiti int.MaxValue kao veličinu stranice
            var pagedResult = GetPaged(1, int.MaxValue);

            if (pagedResult.IsFailed)
            {
                return Result.Fail<List<PreferenceDto>>(pagedResult.Errors);
            }

            return Result.Ok(pagedResult.Value.Results);
        }

         public Result<PagedResult<PreferenceDto>> GetPaged(int pageIndex, int pageSize)
         {
             return base.GetPaged(pageIndex, pageSize);
         }

        // Metoda za paginaciju po ID-u korisnika
        public async Task<Result<PagedResult<PreferenceDto>>> GetPagedByUserId(long touristId, int pageIndex, int pageSize)
        {
            var pagedResult = base.GetPaged(pageIndex, pageSize);

            if (pagedResult.IsFailed)
            {
                return Result.Fail<PagedResult<PreferenceDto>>(pagedResult.Errors);
            }

            // Filtriranje rezultata prema ID-u turiste
            var userPreferences = pagedResult.Value.Results
                                               .Where(p => p.TouristId == touristId)
                                               .ToList();

            var totalCount = userPreferences.Count;

            // Ove dve linije možda treba prilagoditi u zavisnosti od vašeg Mapper-a
            var preferenceDtos = _mapper.Map<List<PreferenceDto>>(userPreferences);
            var pagedResultForUser = new PagedResult<PreferenceDto>(preferenceDtos, totalCount);

            return Result.Ok(pagedResultForUser);
        }
    }







}

