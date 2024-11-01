using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Utilities;
using FluentResults;
using System.Collections.Generic;
using System.Linq;

namespace Explorer.Tours.Core.UseCases.Administration;

public class TourService : BaseService<TourDto, Tour>, ITourService {
    private readonly ITourRepository _repository;
    private readonly IMapper equipmentMapper;

    public TourService(ITourRepository repository, IMapper mapper) : base(mapper) {
        _repository = repository;
        equipmentMapper = new MapperConfiguration(cfg => cfg.CreateMap<Equipment, EquipmentDto>()).CreateMapper();
    }

    public Result<TourDto> GetById(int id) {
        try {
            var tour = _repository.GetById(id);
            var tourDto = MapTourToDto(tour);
            return Result.Ok(tourDto);
        }
        catch (Exception e) {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<List<TourDto>> GetByAuthorId(int id) {
        try {
            var tours = _repository.GetByAuthorId(id);
            var tourDtos = MapToursToDtos(tours);
            return Result.Ok(tourDtos);
        }
        catch (Exception e) {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<TourDto> Create(TourCreationDto tourDto) {
        try {
            var tour = MapTourCreationDtoToEntity(tourDto);
            var returnedTour = _repository.Create(tour);
            var result = MapTourToDto(returnedTour);
            return Result.Ok(result);
        }
        catch (Exception e) {
            return Result.Fail(FailureCode.Internal).WithError(e.Message);
        }
    }

    public Result<TourDto> Update(TourDto tourDto, long id) {
        try {
            tourDto.Id = id;
            var tour = MapTourUpdateDtoToEntity(tourDto);
            var returnedTour = _repository.Update(tour);
            var result = MapTourToDto(returnedTour);
            return Result.Ok(result);
        }
        catch (Exception e) {
            return Result.Fail(FailureCode.Internal).WithError(e.Message);
        }
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

    public Result UpdateTourEquipment(long tourId, List<long> equipmentId) {
        var result = _repository.UpdateTourEquipment(tourId, equipmentId);
        return result;
    }

    public Result<PagedResult<EquipmentDto>> GetTourEquipment(long tourId) {
        var result = _repository.GetTourEquipment(tourId);
        var dtos = equipmentMapper.Map<List<EquipmentDto>>(result.Value.Results);
        return new PagedResult<EquipmentDto>(dtos, dtos.Count);
    }

    public Result<List<TourCardDto>> GetPublishedPagedTours(int page, int pageSize)
    {
        try
        {
            var tours = _repository.GetPublishedPaged(page, pageSize);

            var resultDtos = new List<TourCardDto>();

            foreach(var tour in tours)
            {
                var price = new MoneyDto(tour.Price.Amount, tour.Price.Currency);
                var kp = tour.KeyPoints[0];
                if (kp == null)
                    throw new Exception("Keypoints list is empty!");
                var imgString = Base64Converter.ConvertFromByteArray(kp.Image);
                var firstKeypointDto = new KeyPointDto(kp.Id, kp.Latitude, kp.Longitude, kp.Name, kp.Description, imgString, kp.TourId);

                resultDtos.Add(new TourCardDto(tour.Id, tour.Name, tour.Tags, tour.Level, tour.Status, price, tour.AuthorId, tour.Length, tour.PublishedTime, firstKeypointDto));
                
            }

            return Result.Ok(resultDtos);
        }
        catch (Exception ex)
        {
            return Result.Fail("Failed to get paged tours " + ex.Message);
        }
    }

    private TourDto MapTourToDto(Tour tour) {
        var price = new MoneyDto(tour.Price.Amount, tour.Price.Currency);
        var transportDurationDtos = new List<TransportDurationDto>();
        var keyPoints = new List<KeyPointDto>();
        var reviews = new List<TourReviewDto>();

        foreach (var tr in tour.TransportDurations ?? Enumerable.Empty<TransportDuration>()) {  //This code: "?? Enumerable.Empty<Something>" Makes foreach skip when list is null and doesn't drop exception
            var trDto = new TransportDurationDto(tr.Duration, tr.Transport);
            transportDurationDtos.Add(trDto);
        }

        foreach (var kp in tour.KeyPoints ?? Enumerable.Empty<KeyPoint>()) {
            var img = Base64Converter.ConvertFromByteArray(kp.Image);
            var k = new KeyPointDto(kp.Id, kp.Latitude, kp.Longitude, kp.Name, kp.Description, img, kp.TourId);
            keyPoints.Add(k);
        }

        foreach (var re in tour.Reviews ?? Enumerable.Empty<TourReview>()) {
            var img = Base64Converter.ConvertFromByteArray(re.Image);
            var r = new TourReviewDto(re.Id, re.Rating, re.Comment, re.VisitDate, re.ReviewDate, img, re.TourId, re.TouristId);
            reviews.Add(r);
        }

        var result = new TourDto(tour.Id, tour.Name, tour.Description, tour.Tags, tour.Level,
            tour.Status, price, tour.AuthorId, keyPoints, reviews, tour.Length,
            transportDurationDtos, tour.PublishedTime, tour.ArchivedTime);

        return result;
    }

    private Tour MapTourUpdateDtoToEntity(TourDto tDto) {
        var price = new Money(tDto.Price.Amount, tDto.Price.Currency);
        var transportDurations = new List<TransportDuration>();
        var keyPoints = new List<KeyPoint>();
        var reviews = new List<TourReview>();

        foreach (var trDto in tDto.TransportDurationDtos ?? Enumerable.Empty<TransportDurationDto>()) {
            var tr = new TransportDuration(trDto.Duration, trDto.Transport);
            transportDurations.Add(tr);
        }

        foreach (var kp in tDto.KeyPoints ?? Enumerable.Empty<KeyPointDto>()) {
            var img = Base64Converter.ConvertToByteArray(kp.Image);
            var k = new KeyPoint(kp.Id ,kp.Name, kp.Description, kp.Latitude, kp.Longitude, img, kp.TourId);
            keyPoints.Add(k);
        }

        foreach (var re in tDto.Reviews ?? Enumerable.Empty<TourReviewDto>()) {
            var img = Base64Converter.ConvertToByteArray(re.Image);
            var r = new TourReview(re.Rating, re.Comment, re.VisitDate, re.ReviewDate, re.TourId, re.TouristId, img);
            reviews.Add(r);
        }

        var result = new Tour(tDto.Id, tDto.Name, tDto.Description, tDto.Tags,
            tDto.Level, tDto.Status, price, tDto.AuthorId, keyPoints, reviews, tDto.Length,
            transportDurations, tDto.PublishedTime, tDto.ArchivedTime);

        return result;
    }

    public Tour MapTourCreationDtoToEntity(TourCreationDto tDto) {
        var transportDurations = new List<TransportDuration>();
        var keyPoints = new List<KeyPoint>();

        foreach (var trDto in tDto.TransportDurationDtos ?? Enumerable.Empty<TransportDurationDto>()) {
            var tr = new TransportDuration(trDto.Duration, trDto.Transport);
            transportDurations.Add(tr);
        }

        foreach (var kp in tDto.KeyPoints ?? Enumerable.Empty<KeyPointDto>()) {
            var img = Base64Converter.ConvertToByteArray(kp.Image);
            var k = new KeyPoint(kp.Name, kp.Description, kp.Latitude, kp.Longitude, img, kp.TourId);
            keyPoints.Add(k);
        }

        var result = new Tour(tDto.Name, tDto.Description, tDto.Level, tDto.Tags,
            tDto.AuthorId, keyPoints, tDto.Length, transportDurations);

        return result;
    }

    private List<TourDto> MapToursToDtos(List<Tour> tours) {
        List<TourDto> tourDtos = new List<TourDto>();
        foreach (var t in tours) {
            tourDtos.Add(MapTourToDto(t));
        }
        return tourDtos;
    }
}
