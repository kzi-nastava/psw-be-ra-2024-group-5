using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourLifecycle;
using FluentResults;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface ITourRepository : ICrudRepository<Tour> {
    Result UpdateTourEquipment(long tourId, List<long> equipmentIds);
    Result<PagedResult<Equipment>> GetTourEquipment(long tourId);
    Tour? GetById(long id);
    List<Tour>? GetByAuthorPaged(int authorId, int page, int pageSize);
    List<Tour> GetAuthorPagedToursFiltered(int authorId, int page, int pageSize, double startLong, double endLong, double startLat, double endLat);

    List<Tour> GetPublishedPaged(int page, int pageSize);
   List<Tour> GetPublishedPagedFiltered(int page, int pageSize, double? startLong, double? endLong, double? startLat, double? endLat, string? name, double? length, decimal? minPrice, decimal? maxPrice); 
	Task<List<Tour>> GetToursByIds(List<long> tourIds);

}