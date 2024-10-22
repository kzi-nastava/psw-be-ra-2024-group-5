using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System.Net;

namespace Explorer.Tours.Core.UseCases.Administration;

public class KeyPointService : BaseService<KeyPointDto, KeyPoint>, IKeyPointService {
    private readonly ICrudRepository<KeyPoint> _keyPointRepository;
    private readonly ICrudRepository<Tour> _tourRepository;
    public KeyPointService(ICrudRepository<KeyPoint> keyPointRepository, ICrudRepository<Tour> tourRepository, IMapper mapper) : base(mapper) { 
        _keyPointRepository = keyPointRepository;
        _tourRepository = tourRepository;
    }

    public Result<List<KeyPointDto>> Create(int tourId, List<KeyPointDto> keyPointDtos) {
        try {
            List<KeyPointDto> retVal = new List<KeyPointDto>();
            _tourRepository.Get(tourId);

            foreach (var keyPointDto in keyPointDtos) {
                keyPointDto.TourId = tourId;
                var savedKeyPoint = _keyPointRepository.Create(MapToDomain(keyPointDto));
                retVal.Add(MapToDto(savedKeyPoint));
            }
            return retVal;
        }
        catch (KeyNotFoundException e) {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
        catch (ArgumentException e) {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }

    public Result<PagedResult<KeyPointDto>> GetPaged(int tourId, int page, int pageSize) {
        try {
            _tourRepository.Get(tourId);
        }
        catch (KeyNotFoundException e) {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }

        var result = _keyPointRepository.GetPaged(page, pageSize);

        List<KeyPoint> keyPoints = new List<KeyPoint>();
            
        foreach (var keyPoint in result.Results) { 
            if (keyPoint.TourId == tourId) 
                keyPoints.Add(keyPoint);
        }

        PagedResult<KeyPoint> filteredResult = new PagedResult<KeyPoint>(keyPoints, keyPoints.Count);

        return MapToDto(filteredResult);
    }
}