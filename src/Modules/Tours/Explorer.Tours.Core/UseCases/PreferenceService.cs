using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using FluentResults;
using Explorer.Preferences.Core.Domain.RepositoryInterfaces;
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
        private readonly IPreferenceRepository _preferenceRepository;


        public PreferenceService(IPreferenceRepository preferenceRepository, IMapper mapper) : base(preferenceRepository, mapper)
        {
            _mapper = mapper;
            _preferenceRepository = preferenceRepository;
        }

        public Result<List<PreferenceDto>> GetAll()
        {
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

        public async Task<Result<PagedResult<PreferenceDto>>> GetPagedByUserId(long touristId, int pageIndex, int pageSize)
        {
            var pagedResult = base.GetPaged(pageIndex, pageSize);

            if (pagedResult.IsFailed)
            {
                return Result.Fail<PagedResult<PreferenceDto>>(pagedResult.Errors);
            }

            var userPreferences = pagedResult.Value.Results
                                               .Where(p => p.TouristId == touristId)
                                               .ToList();

            var totalCount = userPreferences.Count;

            var preferenceDtos = _mapper.Map<List<PreferenceDto>>(userPreferences);
            var pagedResultForUser = new PagedResult<PreferenceDto>(preferenceDtos, totalCount);

            return Result.Ok(pagedResultForUser);
        }

        public Result ActivatePreference(long preferenceId)
        {
            var preference = _preferenceRepository.GetById(preferenceId);
            if (preference == null)
            {
                return Result.Fail("Preference not found.");
            }

            preference.Activate();
            _preferenceRepository.Update(preference);
            return Result.Ok();
        }

        public Result DeactivatePreference(long preferenceId)
        {
            var preference = _preferenceRepository.GetById(preferenceId);
            if (preference == null)
            {
                return Result.Fail("Preference not found.");
            }

            preference.Deactivate();
            _preferenceRepository.Update(preference);
            return Result.Ok();
        }

        public Result DeletePreference(long preferenceId)
        {
            var preference = _preferenceRepository.GetById(preferenceId);
            if (preference == null)
            {
                return Result.Fail("Preference not found.");
            }

            _preferenceRepository.Delete(preferenceId);
            return Result.Ok();
        }

        public Result<List<PreferenceDto>> GetAllByTouristId(long touristId)
        {
            var allPreferences = _preferenceRepository.GetAll()
;
            if (allPreferences == null || !allPreferences.Any())
            {
                return Result.Fail<List<PreferenceDto>>("No preferences found.");
            }

            var userPreferences = allPreferences.Where(p => p.TouristId == touristId).ToList();

            if (!userPreferences.Any())
            {
                return Result.Fail<List<PreferenceDto>>("No preferences found for this tourist.");
            }

            var preferenceDtos = _mapper.Map<List<PreferenceDto>>(userPreferences);

            return Result.Ok(preferenceDtos);
        }

    }







}

