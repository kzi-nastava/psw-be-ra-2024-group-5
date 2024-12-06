using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AppRatingService : CrudService<AppRatingDto, AppRating>, IAppRatingService
    {
        private readonly IAppRatingRepository _appRatingRepository;
        private readonly IMapper _mapper;

        public AppRatingService(IAppRatingRepository appRatingRepository, ICrudRepository<AppRating> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _appRatingRepository = appRatingRepository;
            _mapper = mapper;
        }

        public Result<AppRatingDto> GetByUser(long userId)
        {
            try
            {
                var rating = _appRatingRepository.GetByUser(userId);
                if (rating == null)
                {
                    return Result.Fail("Rating not found");
                }
                return Result.Ok(_mapper.Map<AppRatingDto>(rating));
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }

        public override Result<AppRatingDto> Create(AppRatingDto appRatingDto)
        {
            try
            {
                var existingRating = _appRatingRepository.GetByUser(appRatingDto.UserId);
                appRatingDto.TimeStamp = DateTime.UtcNow;  // Set timestamp

                if (existingRating != null)
                {
                    appRatingDto.Id = existingRating.Id;
                    base.Delete((int)existingRating.Id);
                }

                return base.Create(appRatingDto);  // Keep using base.Create
            }
            catch (Exception e)
            {
                return Result.Fail(e.Message);
            }
        }
    }
}