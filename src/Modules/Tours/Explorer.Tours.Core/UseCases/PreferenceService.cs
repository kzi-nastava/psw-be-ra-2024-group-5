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
        public PreferenceService(ICrudRepository<Preference> repository, IMapper mapper) : base(repository, mapper) { }

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




    }
}
