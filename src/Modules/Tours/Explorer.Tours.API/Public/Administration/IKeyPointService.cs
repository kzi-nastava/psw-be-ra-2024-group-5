using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration;

public interface IKeyPointService {
    public Result<List<KeyPointDto>> Create(int tourId, List<KeyPointDto> keyPointDtos);
    public Result<PagedResult<KeyPointDto>> GetPaged(int tourId, int page, int pageSize);
}