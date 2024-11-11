using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Dtos;

using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Utilities;
using FluentResults;
using System.Collections.Generic;
using System.Linq;
using Explorer.Tours.API.Enum;

namespace Explorer.Tours.Core.UseCases.Administration;

public class TourService : BaseService<TourDto, Tour>, ITourService {
    private readonly ITourRepository _tourRepository;
    private readonly IMapper equipmentMapper;
    protected readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly ITourExecutionRepository _tourExecutionRepository;

    public TourService(ITourRepository repository, IUserRepository userRepository , IMapper mapper, IShoppingCartRepository shoppingCartRepository, ITourExecutionRepository tourExecutionRepository) : base(mapper)
    {
        _tourRepository = repository;
        _tourExecutionRepository = tourExecutionRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _shoppingCartRepository = shoppingCartRepository;
        _tourExecutionRepository = tourExecutionRepository;
        equipmentMapper = new MapperConfiguration(cfg => cfg.CreateMap<Equipment, EquipmentDto>()).CreateMapper();
    }

    public Result<TourDto> GetById(int id) {
        try {
            var tour = _tourRepository.GetById(id);
            var tourDto = MapTourToDto(tour);
            return Result.Ok(tourDto);
        }
        catch (Exception e) {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }



    public Result<List<TourDto>> GetByAuthorId(int id) {
        try {
            var tours = _tourRepository.GetByAuthorId(id);
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
            var returnedTour = _tourRepository.Create(tour);
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
            var returnedTour = _tourRepository.Update(tour);
            var result = MapTourToDto(returnedTour);
            return Result.Ok(result);
        }
        catch (Exception e) {
            return Result.Fail(FailureCode.Internal).WithError(e.Message);
        }
    }

    public Result Delete(int id) {
        try {
            _tourRepository.Delete(id);
            return Result.Ok();
        }
        catch (KeyNotFoundException e) {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result UpdateTourEquipment(long tourId, List<long> equipmentId) {
        var result = _tourRepository.UpdateTourEquipment(tourId, equipmentId);
        return result;
    }

    public Result<PagedResult<EquipmentDto>> GetTourEquipment(long tourId) {
        var result = _tourRepository.GetTourEquipment(tourId);
        var dtos = equipmentMapper.Map<List<EquipmentDto>>(result.Value.Results);
        return new PagedResult<EquipmentDto>(dtos, dtos.Count);
    }

    public Result<List<TourCardDto>> GetPublishedPagedTours(int page, int pageSize)
    {
        try
        {
            var tours = _tourRepository.GetPublishedPaged(page, pageSize);

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
            var r = new TourReviewDto(re.Id, re.Rating, re.Comment, re.VisitDate, re.ReviewDate, img, re.TourId, re.TouristId, re.CompletionPercentage);
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
            var r = new TourReview(re.Rating, re.Comment, re.VisitDate, re.ReviewDate, re.TourId, re.TouristId, img, re.CompletionPercentage);
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

    public Result<TourTouristDto> GetForTouristById(long tourId, long touristId)
    {
        try
        {
            var tour = _tourRepository.GetById((int)tourId);
            if (tour == null) return Result.Fail("Tour not found");

            var tourDto = MapTourToDto(tour);
            var tourTouristDto = new TourTouristDto(tourDto);

            var tourExecution = _tourExecutionRepository.GetByTourAndUser(tourId, touristId);

            SetTouristPermissions(tourTouristDto, tourExecution, tour,touristId);

            return Result.Ok(tourTouristDto);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    private void SetTouristPermissions(TourTouristDto tourTouristDto, TourExecution tourExecution, Tour tour, long touristId)
    {

        tourTouristDto.CanBeBought = tour.Status == TourStatus.Published && 
                                     !_shoppingCartRepository.IsTourBought(touristId, tour.Id);

        tourTouristDto.CanBeActivated = tourExecution == null &&
                                        _shoppingCartRepository.IsTourBought(touristId, tour.Id);

        if (tourExecution != null)
        {
            var completionPercentage = CalculateCompletionPercentage(tourExecution, tour);
            var isWithinTimeLimit = IsWithinTimeLimit(tourExecution);

            tourTouristDto.CanBeReviewed = completionPercentage >= 35 &&
                                           isWithinTimeLimit &&
                                           !tour.Reviews.Any(r => r.TouristId == tourExecution.UserId);
        }
    }
    private bool IsWithinTimeLimit(TourExecution execution)
    {
        if (execution.LastActivity == null) return false;

        var daysSinceLastActivity = (DateTime.UtcNow - execution.LastActivity.Value).TotalDays;
        return daysSinceLastActivity <= 7;
    }

    private double CalculateCompletionPercentage(TourExecution execution, Tour tour)
    {
        var completedPoints = execution.KeyPointProgresses.Count;
        var totalPoints = tour.KeyPoints.Count;

        if (totalPoints == 0) return 0;
        return (completedPoints * 100.0) / totalPoints;
    }

    public Result<TourReviewDto> AddReview(TourReviewDto reviewDto)
    {
        try
        {
            var tour = _tourRepository.GetById((int)reviewDto.TourId);
            if (tour == null) return Result.Fail("Tour not found");

            var tourExecution = _tourExecutionRepository.GetByTourAndUser((int)reviewDto.TourId, (int)reviewDto.TouristId);
            if (tourExecution == null) return Result.Fail("No tour execution found");

            var tourTouristResult = GetForTouristById((int)reviewDto.TourId, (int)reviewDto.TouristId);
            if (tourTouristResult.IsFailed) return Result.Fail(tourTouristResult.Errors);

            if (!tourTouristResult.Value.CanBeReviewed)
                return Result.Fail("Tourist cannot review this tour");

            var completionPercentage = CalculateCompletionPercentage(tourExecution, tour);

            var reviewDate = DateTime.UtcNow;
            var review = new TourReview(
                reviewDto.Rating,
                reviewDto.Comment,
                reviewDto.VisitDate,
                reviewDate,
                reviewDto.TourId,
                reviewDto.TouristId,
                Base64Converter.ConvertToByteArray(reviewDto.Image)
            );
            review.CompletionPercentage = completionPercentage;

            tour.AddReview(review);
            _tourRepository.Update(tour);

            return Result.Ok(_mapper.Map<TourReviewDto>(review));
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result PublishTour(int tourId)
    {
        var tour = _tourRepository.GetById(tourId);
        if (tour == null)
        {
            return Result.Fail(FailureCode.NotFound).WithError("Tour not found.");
        }

        if (!tour.Publish())
        {
            return Result.Fail("Tour cannot be published. Ensure all requirements are met.");
        }

        _tourRepository.Update(tour);
        return Result.Ok();
    }

    public Result ArchiveTour(int tourId)
    {
        var tour = _tourRepository.GetById(tourId);
        if (tour == null)
        {
            return Result.Fail(FailureCode.NotFound).WithError("Tour not found.");
        }
        if (!tour.Archive())
        {
            return Result.Fail("Tour cannot be archived. Ensure all requirements are met.");
        }

        _tourRepository.Update(tour);
        return Result.Ok();
    }
}

