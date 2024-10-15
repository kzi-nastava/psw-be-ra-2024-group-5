using AutoMapper;
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

    public Result<List<KeyPointDto>> Create(List<KeyPointDto> keyPointDtos) {
        List<KeyPoint> keyPoints = MapToDomain(keyPointDtos);
        List<KeyPointDto> retVal = new List<KeyPointDto>();

        foreach (var keyPoint in keyPoints) {
            var savedKeyPoint = _keyPointRepository.Create(keyPoint);
            retVal.Add(MapToDto(savedKeyPoint));
        }

        return retVal;
    }
}