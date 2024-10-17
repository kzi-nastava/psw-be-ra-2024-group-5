using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration;

public class TourService : CrudService<TourDto, Tour>, ITourService {
    public TourService(ICrudRepository<Tour> repository, IMapper mapper) : base(repository, mapper) { }
    public Result<PagedResult<TourDto>> GetByAuthorId(long id) {
        var tours = GetPaged(0, 0);
        if (tours.IsFailed)
            return Result.Fail(tours.Errors);
        var selectedTours = tours.Value.Results.Where(t => t.AuthorId == id).ToList<TourDto>();
        var result = new PagedResult<TourDto>(selectedTours, selectedTours.Count);

        return result;
    }
}
