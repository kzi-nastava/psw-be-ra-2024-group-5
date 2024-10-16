using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class UserProfileService : CrudService<UserProfileDto, UserProfile>, IUserProfileService
{
    public UserProfileService(ICrudRepository<UserProfile> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    public Result<UserProfileDto> Create(UserProfileDto userProfile)
    {
        return base.Create(userProfile);
    }

    public Result<UserProfileDto> Update(UserProfileDto userProfile)
    {
        return base.Update(userProfile);
    }

    Result<UserProfileDto> IUserProfileService.Get(long userId)
    {
        return base.Get((int)userId); //ovo moze biti problem ?
    }
}
