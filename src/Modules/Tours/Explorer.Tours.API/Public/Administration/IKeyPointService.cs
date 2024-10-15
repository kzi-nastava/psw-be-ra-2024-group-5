using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;

namespace Explorer.Tours.API.Public.Administration;

public interface IKeyPointService {
    Result<KeyPointDto> Create(KeyPointDto keyPointDto);
}