using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Enum;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.ShoppingCarts;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration;

public class TourService : BaseService<TourDto, Tour>, ITourService 
{
    private readonly ITourRepository _repository;
    private readonly IMapper equipmentMapper;
    private readonly IMapper keyPointMapper;
    private readonly IMapper reviewMapper;
    //private readonly IMapper _mapper;

    public TourService(ITourRepository repository, IMapper mapper) : base(mapper)
    {
        _repository = repository;
        equipmentMapper = new MapperConfiguration(cfg => cfg.CreateMap<Equipment, EquipmentDto>()).CreateMapper();
        keyPointMapper = new MapperConfiguration(cfg => cfg.CreateMap<KeyPointDto, KeyPoint>()).CreateMapper();
        reviewMapper = new MapperConfiguration(cfg => cfg.CreateMap<TourReviewDto, TourReview>()).CreateMapper();
        //_mapper = mapper;
    }
    public Result<TourDto> GetById(int id) {
        var tour = _repository.GetById(id);
        var result = MapTourToDto(tour);
        return result;
    }
    public Result<List<TourDto>> GetByAuthorId(int id) {
        var tours = _repository.GetByAuthorId(id);
        return MapToDto(tours);
    }
    public Result<TourDto> Create(TourDto tourDto) {
        //tour.Status = TourStatus.Draft;
        //tour.Price = new Money(0.0, Currency.Rsd);
        var tour = MapTourToEntity(tourDto);
        var result = _repository.Create(tour);
        return MapTourToDto(result);
    }

    public Result<TourDto> Update(TourDto tourDto, long id) {
        tourDto.Id = id;
        var tour = MapTourToEntity(tourDto);
        var result = _repository.Update(tour);
        return MapTourToDto(result);
    }

    public Result Delete(int id) {
        try {
            _repository.Delete(id);
            return Result.Ok();
        }
        catch (KeyNotFoundException e) {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result UpdateTourEquipment(long tourId, List<long> equipmentId)
    {
        var result = _repository.UpdateTourEquipment(tourId, equipmentId);
        return result;
    }

    public Result<PagedResult<EquipmentDto>> GetTourEquipment(long tourId) {
        var result = _repository.GetTourEquipment(tourId); 
        var dtos = equipmentMapper.Map<List<EquipmentDto>>(result.Value.Results);

        return new PagedResult<EquipmentDto>(dtos, dtos.Count);
    }

    private TourDto MapTourToDto(Tour tour) {
        var Price = new MoneyDto(tour.Price.Amount, tour.Price.Currency);

        var keyPoints = new List<KeyPointDto>();
        var reviews = new List<TourReviewDto>();

        //foreach (var kp in tour.KeyPoints) {
        //    var k = keyPointMapper.Map<KeyPointDto>(kp);
        //    keyPoints.Add(k);
        //}
        //foreach (var re in tour.Reviews) {
        //    var r = reviewMapper.Map<TourReviewDto>(re);
        //    reviews.Add(r); 
        //}

        var result = new TourDto(tour.Id, tour.Name, tour.Description, tour.Tags, tour.Level, tour.Status, Price, tour.AuthorId, keyPoints, reviews, tour.Length, tour.Transport, tour.Duration, tour.PublishedTime, tour.ArchivedTime);

        return result;
    }
    private Tour MapTourToEntity(TourDto tDto) {
        var Price = new Money(tDto.Price.Amount, tDto.Price.Currency);

        var keyPoints = new List<KeyPoint>();
        var reviews = new List<TourReview>();

        //foreach (var kp in tDto.KeyPoints) {
        //    var k = keyPointMapper.Map<KeyPoint>(kp);
        //    keyPoints.Add(k);
        //}
        //foreach (var re in tDto.Reviews) {
        //    var r = reviewMapper.Map<TourReview>(re);
        //    reviews.Add(r);
        //}

        var result = new Tour(tDto.Name, tDto.Description, tDto.Level, tDto.Tags, tDto.AuthorId, keyPoints, reviews, tDto.Length, tDto.Transport, tDto.Duration);

        return result;
    }
}

