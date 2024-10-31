using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Utilities;
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration;

public class TourService : BaseService<TourDto, Tour>, ITourService 
{
    private readonly ITourRepository _repository;
    private readonly IMapper equipmentMapper;

    public TourService(ITourRepository repository, IMapper mapper) : base(mapper)
    {
        _repository = repository;
        equipmentMapper = new MapperConfiguration(cfg => cfg.CreateMap<Equipment, EquipmentDto>()).CreateMapper();
    }
    public Result<TourDto> GetById(int id) {
        var tour = _repository.GetById(id);
        var result = MapTourToDto(tour);
        return result;
    }
    public Result<List<TourDto>> GetByAuthorId(int id) {
        var tours = _repository.GetByAuthorId(id);
        var results = MapToursToDtos(tours);
        return results;
    }
    public Result<TourDto> Create(TourDto tourDto) {
        var tour = MapTourToEntity(tourDto, false);
        var result = _repository.Create(tour);
        return MapTourToDto(result);
    }

    public Result<TourDto> Update(TourDto tourDto, long id) {
        tourDto.Id = id;
        var tour = MapTourToEntity(tourDto, true);
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
        //var reviews = new List<TourReviewDto>();

        if (tour.KeyPoints != null) {
            foreach (var kp in tour.KeyPoints) {
                var img = Base64Converter.ConvertFromByteArray(kp.Image);
                var k = new KeyPointDto(kp.Id, kp.Latitude, kp.Longitude, kp.Name, kp.Description, img, kp.TourId);
                keyPoints.Add(k);
            }
        }

        //if (tour.Reviews != null) {
        //    foreach (var re in tour.Reviews) {
        //        var img = Base64Converter.ConvertFromByteArray(re.Image);
        //        var r = new TourReviewDto(re.Id, re.Rating, re.Comment, re.VisitDate, re.ReviewDate, img, re.TourId, re.TouristId);
        //        reviews.Add(r);
        //    }
        //}


        var result = new TourDto(tour.Id, tour.Name, tour.Description, tour.Tags, tour.Level, tour.Status, Price, tour.AuthorId, keyPoints, tour.Length, tour.Transport, tour.Duration, tour.PublishedTime, tour.ArchivedTime);

        return result;
    }

    private Tour MapTourToEntity(TourDto tDto, bool isUpdate) {
        var Price = new Money(tDto.Price.Amount, tDto.Price.Currency);

        var keyPoints = new List<KeyPoint>();
        //var reviews = new List<TourReview>();

        if(tDto.KeyPoints != null) { 
        foreach (var kp in tDto.KeyPoints) {
                var img = Base64Converter.ConvertToByteArray(kp.Image);
                var k = new KeyPoint(kp.Name, kp.Description, kp.Latitude, kp.Longitude, img, kp.TourId);
                keyPoints.Add(k);
            }
        }
        //if(tDto.Reviews != null) {
        //    foreach (var re in tDto.Reviews) {
        //        var img = Base64Converter.ConvertToByteArray(re.Image);
        //        var r = new TourReview(re.Rating, re.Comment, re.VisitDate, re.ReviewDate, re.TourId, re.TouristId, img);
        //        reviews.Add(r);
        //    }
        //}
        var result = new Tour();

        if (isUpdate) {
            result = new Tour(tDto.Id, tDto.Name, tDto.Description, tDto.Tags, tDto.Level, tDto.Status, Price, tDto.AuthorId, keyPoints, tDto.Length, tDto.Transport, tDto.Duration, tDto.PublishedTime, tDto.ArchivedTime);

        }
        else {
            result = new Tour(tDto.Name, tDto.Description, tDto.Level, tDto.Tags, tDto.AuthorId, keyPoints, tDto.Length, tDto.Transport, tDto.Duration);
        }


        return result;
    }

    private List<TourDto> MapToursToDtos(List<Tour> tours) {
        List<TourDto> tourDtos = new List<TourDto>();
        foreach(var t in tours) {
            tourDtos.Add(MapTourToDto(t));
        }
        return tourDtos;
    }

    public Result PublishTour(int tourId)
    {
        var tour = _repository.GetById(tourId);
        if (tour == null)
        {
            return Result.Fail(FailureCode.NotFound).WithError("Tour not found.");
        }

        if (!tour.Publish())
        {
            return Result.Fail("Tour cannot be published. Ensure all requirements are met.");
        }

        _repository.Update(tour);
        return Result.Ok();
    }
}

