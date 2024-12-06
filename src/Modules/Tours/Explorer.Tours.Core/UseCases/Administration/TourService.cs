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
using Explorer.Tours.API.Dtos.TourLifecycle;
using Explorer.Tours.API.Internal;
using Explorer.Payments.API.Internal;
using Explorer.Tours.API.Dtos.TourLeaderboard;
using Explorer.Stakeholders.API.Internal;
using Explorer.Preferences.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Core.UseCases.Administration;

public class TourService : ITourService {
    private readonly ITourRepository _tourRepository;
    private readonly IMapper equipmentMapper;
    protected readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IInternalShoppingCartService _shoppingService;
    private readonly ITourExecutionRepository _tourExecutionRepository;
    private readonly ITourReviewRepository _tourReviewRepository;
    private readonly IUserProfileServiceInternal _userProfileService;
    private readonly IPreferenceRepository _preferenceRepository;

    public TourService(ITourRepository repository, IUserRepository userRepository , IMapper mapper,
        IInternalShoppingCartService shoppingCartRepository, ITourExecutionRepository tourExecutionRepository,
        ITourReviewRepository tourReviewRepository, IUserProfileServiceInternal userProfileService, IPreferenceRepository preferenceRepository)
    {
        _tourRepository = repository;
        _tourExecutionRepository = tourExecutionRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _shoppingService = shoppingCartRepository;
        _tourExecutionRepository = tourExecutionRepository;
        equipmentMapper = new MapperConfiguration(cfg => cfg.CreateMap<Equipment, EquipmentDto>()).CreateMapper();
        _tourReviewRepository = tourReviewRepository;
        _userProfileService = userProfileService;
        _preferenceRepository = preferenceRepository;
    }

    public Result<TourDto> GetById(long id) {
        try {
            var tour = _tourRepository.GetById(id);
            var tourDto = _mapper.Map<TourDto>(tour);
            return Result.Ok(tourDto);
        }
        catch (Exception e) {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }

    public Result<List<TourCardDto>> GetByAuthorPaged(int authorId, int page, int pageSize) { 
        try {
            var tours = _tourRepository.GetByAuthorPaged(authorId, page, pageSize);
            var tourDtos = _mapper.Map<List<TourCardDto>>(tours);

            return Result.Ok(tourDtos);
        }
        catch (Exception e) {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }
    public Result<List<TourCardDto>> GetAuthorPagedToursFiltered(int authorId, int page, int pageSize, double startLong, double endLong, double startLat, double endLat) {
        try {
            var filteredTours = _tourRepository.GetAuthorPagedToursFiltered(authorId, page, pageSize, startLong, endLong, startLat, endLat);
            var tourDtos = _mapper.Map<List<TourCardDto>>(filteredTours);
            return Result.Ok(tourDtos);
        }
        catch (Exception e) {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
    }


    public Result<TourDto> Create(TourCreationDto tourDto) {
        try {
            var tour = _mapper.Map<Tour>(tourDto);
            var returnedTour = _tourRepository.Create(tour);
            var result = _mapper.Map<TourDto>(tour);
            return Result.Ok(result);
        }
        catch (Exception e) {
            return Result.Fail(FailureCode.Internal).WithError(e.Message);
        }
    }

    public Result<TourDto> Update(TourDto tourDto, long id) {
        try {
            tourDto.Id = id;
            var tour = _mapper.Map<Tour>(tourDto);
            var returnedTour = _tourRepository.Update(tour);
            var result = _mapper.Map<TourDto>(tour);
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
                var firstKeypointDto = new KeyPointDto(kp.Id, kp.Latitude, kp.Longitude, kp.Name, kp.Description, kp.TourId);
                firstKeypointDto.Image = imgString;
                var reviews = _tourReviewRepository.GetByTourId((int)tour.Id);
                double? averageRating = null;

                if (reviews.IsSuccess && reviews.Value.Any())
                {
                    
                    averageRating = reviews.Value.Average(r => r.Rating);
                }
                resultDtos.Add(new TourCardDto(
                                tour.Id,
                                tour.Name,
                                tour.Tags,
                                tour.Level,
                                tour.Status,
                                price,
                                tour.AuthorId,
                                tour.Length,
                                tour.PublishedTime,
                                firstKeypointDto,
                                averageRating 
                            ));
            }

            return Result.Ok(resultDtos);
        }
        catch (Exception ex)
        {
            return Result.Fail("Failed to get paged tours " + ex.Message);
        }
    }


    public Result<List<TourCardDto>> GetPublishedPagedToursFiltered(int page, int pageSize, double? startLong, double? endLong, double? startLat, double? endLat, string? name, double? length, decimal? minPrice, decimal? maxPrice)
    {
        try
        {
            var tours = _tourRepository.GetPublishedPagedFiltered(page, pageSize, startLong, endLong, startLat, endLat, name, length, minPrice, maxPrice);
            var resultDtos = new List<TourCardDto>();

            foreach (var tour in tours)
            {
                var price = new MoneyDto(tour.Price.Amount, tour.Price.Currency);
                var kp = tour.KeyPoints[0];
                if (kp == null)
                    throw new Exception("Keypoints list is empty!");

                var imgString = Base64Converter.ConvertFromByteArray(kp.Image);
                var firstKeypointDto = new KeyPointDto(kp.Id, kp.Latitude, kp.Longitude, kp.Name, kp.Description, kp.TourId);
                firstKeypointDto.Image = imgString;

                var reviews = _tourReviewRepository.GetByTourId((int)tour.Id);
                double? averageRating = null;
                if (reviews.IsSuccess && reviews.Value.Any())
                {
                    averageRating = reviews.Value.Average(r => r.Rating);
                }

                resultDtos.Add(new TourCardDto(
                    tour.Id,
                    tour.Name,
                    tour.Tags,
                    tour.Level,
                    tour.Status,
                    price,
                    tour.AuthorId,
                    tour.Length,
                    tour.PublishedTime,
                    firstKeypointDto,
                    averageRating
                ));
            }

            return Result.Ok(resultDtos);
        }
        catch (Exception ex)
        {
            return Result.Fail("Failed to get paged tours " + ex.Message);
        }
    }

    public Result<TourTouristDto> GetForTouristById(long tourId, long touristId)
    {
        try
        {
            var user = this._userRepository.Get(touristId);

            if (user == null || user.Role != Stakeholders.Core.Domain.UserRole.Tourist)
            {
                return Result.Fail(FailureCode.Forbidden);
            }

            var tour = _tourRepository.GetById(tourId);

            if (tour == null || tour.Status != API.Enum.TourStatus.Published)
            {
                return Result.Fail(FailureCode.InvalidArgument);
            }

            var tourDto = _mapper.Map<TourDto>(tour);
            var tourTouristDto = new TourTouristDto(tourDto);

            var activeTour = _tourExecutionRepository.GetActive(touristId);
            bool isTourInCart = _shoppingService.IsTourInCart(touristId, tourId);
            bool isTourBought = _shoppingService.IsTourBought(touristId, tourId);

            //dok ne kupi ne moze da vidi sve keypointove
            if (!isTourBought)
            {
                tourTouristDto.Tour.KeyPoints = new List<KeyPointDto>
                {
                    tourTouristDto.Tour.KeyPoints[0]
                };

                if (!isTourInCart)
                    tourTouristDto.CanBeBought = true;
                

                return Result.Ok(tourTouristDto);
            }
            else {
                //moze da aktivira
                if (activeTour == null)
                {
                    tourTouristDto.CanBeActivated = true;
                }

                var recentExecutions = _tourExecutionRepository.GetRecentByTourAndUser(tourId, touristId);

                foreach (var tourExecution in recentExecutions)
                {
                    var completionPercentage = CalculateCompletionPercentage(tourExecution, tour);
                    bool tourReviewFound = !tour.Reviews.Any(r => r.TouristId == tourExecution.UserId);

                    if (completionPercentage >= 35 && tourReviewFound)
                    {
                        tourTouristDto.CanBeReviewed = true;
                        break;
                    }
                }

                return Result.Ok(tourTouristDto);
            }
        }
        catch (Exception e)
        {
            return Result.Fail(FailureCode.NotFound).WithError(e.Message);
        }
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
            var tour = _tourRepository.GetById(reviewDto.TourId);
            if (tour == null) return Result.Fail("Tour not found");

            var tourExecutions = _tourExecutionRepository.GetRecentByTourAndUser((int)reviewDto.TourId, (int)reviewDto.TouristId);
            if (tourExecutions.Count == 0) return Result.Fail("No tour execution found");

            var tourTouristResult = GetForTouristById((int)reviewDto.TourId, (int)reviewDto.TouristId);
            if (tourTouristResult.IsFailed) return Result.Fail(tourTouristResult.Errors);

            if (!tourTouristResult.Value.CanBeReviewed)
                return Result.Fail("Tourist cannot review this tour");

            var maxCompletion = tourExecutions.Max(te => CalculateCompletionPercentage(te, tour));

            var reviewDate = DateTime.UtcNow;
            var review = new TourReview(
                reviewDto.Rating,
                reviewDto.Comment,
                reviewDto.VisitDate,
                reviewDate,
                Base64Converter.ConvertToByteArray(reviewDto.Image),
                reviewDto.TourId,
                reviewDto.TouristId
            );
            review.CompletionPercentage = maxCompletion;

            tour.AddReview(review);
            _tourRepository.Update(tour);

            return Result.Ok(_mapper.Map<TourReviewDto>(review));
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<TourLeaderboardDto> GetLeaderboard(int tourId, int size = 10)
    {
        try
        {
            var tour = _tourRepository.GetById(tourId);

            if (tour == null) 
                return Result.Fail(FailureCode.NotFound).WithError($"Tour not with id:{tourId} found");

            var completedExecutions = _tourExecutionRepository.GetByTour(tourId).Where(te => te.IsCompleted());

            var sortedExecutions = completedExecutions.GroupBy(te => te.UserId)
                .Select(group => group.OrderBy(te => te.GetCompletionDuration()).First())
                .OrderBy(te => te.GetCompletionDuration()).Take(size).ToList();

            var leaderboardEntries = sortedExecutions.Select(
                (te, i) => new LeaderboardEntryDto
                {
                    Position = i + 1,
                    UserId = te.UserId,
                    ProfileImage = Convert.ToBase64String(_userProfileService.GetProfileImageByUserId(te.UserId).Value!),
                    UserName = _userProfileService.GetDisplayNameByUserId(te.UserId).Value,
                    Time = te.GetCompletionDuration()!.Value
                }).ToList();

            return Result.Ok(new TourLeaderboardDto(tourId, leaderboardEntries));

        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result PublishTour(long tourId, double priceAmount, Currency currency)
    {
        var tour = _tourRepository.GetById(tourId);
        if (tour == null)
        {
            return Result.Fail(FailureCode.NotFound).WithError("Tour not found.");
        }

        
        if (!tour.Publish(priceAmount, currency))
        {
            return Result.Fail("Tour cannot be published. Ensure all requirements are met.");
        }

        _tourRepository.Update(tour);

        return Result.Ok();
    }


    public Result ArchiveTour(long tourId)
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

	public async Task<List<TourDto>> GetToursByIds(List<long> tourIds)
	{
		var tours = await _tourRepository.GetToursByIds(tourIds);

		// Pretvaramo Tour objekte u TourDto (ako koristite DTOs za slanje podataka)
		return tours.Select(t => new TourDto
		{
			Id = t.Id,
			Name = t.Name,
			Description = t.Description
		}).ToList();
	}

    public Result<PagedResult<TourCardDto>> GetToursByActivePreferencePaged(long touristId, int page, int pageSize)
    {
        try
        {
            var preferences = _preferenceRepository.GetByTouristId(touristId);
            var activePreference = preferences.FirstOrDefault(p => p.IsActive);

            if (activePreference == null)
            {
                return Result.Fail<PagedResult<TourCardDto>>("No active preferences found.");
            }

            var pagedTours = _tourRepository.GetPublishedPaged(page, pageSize);

            var filteredTours = pagedTours.Where(tour =>
                (tour.Level == activePreference.PreferredDifficulty) ||
                (tour.TransportDurations.Any(td =>
                    (activePreference.WalkRating > 0 && td.Transport == TourTransport.OnFoot) ||
                    (activePreference.BikeRating > 0 && td.Transport == TourTransport.Bicycle) ||
                    (activePreference.CarRating > 0 && td.Transport == TourTransport.Car))) ||
                (activePreference.InterestTags.Any(tag =>
                    !string.IsNullOrEmpty(tour.Tags) && tour.Tags.Split(',').Contains(tag.Trim())))
            ).ToList();

            var tourDtos = filteredTours.Select(tour =>
            {
                var price = new MoneyDto(tour.Price.Amount, tour.Price.Currency);
                var keypoint = tour.KeyPoints.FirstOrDefault();  
                var keyPointDto = keypoint == null ? null : new KeyPointDto(keypoint.Id, keypoint.Latitude, keypoint.Longitude, keypoint.Name, keypoint.Description, keypoint.TourId)
                {
                    Image = keypoint?.Image != null ? Base64Converter.ConvertFromByteArray(keypoint.Image) : null
                };

                var reviews = _tourReviewRepository.GetByTourId((int)tour.Id);
                double? averageRating = reviews.IsSuccess && reviews.Value.Any()
                    ? reviews.Value.Average(r => r.Rating)
                    : null;

                return new TourCardDto(
                    tour.Id,
                    tour.Name,
                    tour.Tags,
                    tour.Level,
                    tour.Status,
                    price,
                    tour.AuthorId,
                    tour.Length,
                    tour.PublishedTime,
                    keyPointDto,
                    averageRating
                );
            }).ToList();

            var pagedResult = new PagedResult<TourCardDto>(tourDtos, filteredTours.Count); 

            return Result.Ok(pagedResult);
        }
        catch (Exception ex)
        {
            return Result.Fail<PagedResult<TourCardDto>>($"An error occurred while filtering tours: {ex.Message}");
        }
    }




}

