using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Enum;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration;

public class TourService : CrudService<TourDto, Tour>, ITourService 
{
    private readonly ITourRepository _repository;
    
    public TourService(ITourRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
    }

    public Result<TourDto> CreateTour(TourDto tour) {
        tour.Status = TourStatus.Draft;
        tour.Price = 0.0;
        return Create(tour);
    }


    public Result<PagedResult<TourDto>> GetByAuthorId(long id) {
        var tours = GetPaged(0, 0);
        if (tours.IsFailed)
            return Result.Fail(tours.Errors);
        var selectedTours = tours.Value.Results.Where(t => t.AuthorId == id).ToList<TourDto>();
        var result = new PagedResult<TourDto>(selectedTours, selectedTours.Count);

        return result;
    }

    public Result UpdateTourEquipment(long tourId, List<long> equipmentId)
    {
        var result = _repository.UpdateTourEquipment(tourId, equipmentId);
        return result;
    }
}

