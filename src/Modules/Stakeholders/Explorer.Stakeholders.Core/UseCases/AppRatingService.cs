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

        public AppRatingService(IAppRatingRepository appRatingRepository, ICrudRepository<AppRating> repository, IMapper mapper)
            : base(repository, mapper)
        {
            _appRatingRepository = appRatingRepository;
        }


        public override Result<AppRatingDto> Create(AppRatingDto appRatingDto)
        {
            var existingRating = _appRatingRepository.GetByUser(appRatingDto.UserId);

            appRatingDto.TimeStamp = DateTime.UtcNow;

            if (existingRating != null)
            {
                appRatingDto.Id = existingRating.Id;
                base.Delete((int)existingRating.Id);

            }

            return base.Create(appRatingDto);

        }
    }
}