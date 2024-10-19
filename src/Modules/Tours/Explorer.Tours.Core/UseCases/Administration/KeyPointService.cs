using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;

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
}