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
    public KeyPointService(ICrudRepository<KeyPoint> keyPointRepository, IMapper mapper) : base(mapper) { 
        _keyPointRepository = keyPointRepository;
    }

    public Result<List<KeyPointDto>> Create(int tourId, List<KeyPointDto> keyPointDtos) {
        try {
            List<KeyPointDto> retVal = new List<KeyPointDto>();

            foreach (var keyPointDto in keyPointDtos) {
                keyPointDto.TourId = tourId;
                var savedKeyPoint = _keyPointRepository.Create(MapToDomain(keyPointDto));
                retVal.Add(MapToDto(savedKeyPoint));
            }
            return retVal;
        }
        catch (ArgumentException e) {
            return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
        }
    }
}