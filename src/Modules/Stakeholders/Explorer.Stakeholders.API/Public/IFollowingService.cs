using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;


namespace Explorer.Stakeholders.API.Public;

public interface IFollowingService
{
    Result<FollowingDto> AddFollower(long userId, long followedUserId);
    Result<FollowingDto> RemoveFollower(long userId, long followedUserId);
    Result<PagedResult<UserProfileDto>> GetPagedFollowersByUserId(long userId, int page, int pageSize);
    bool IsAlreadyFollowing(long userId, long followerId);
}
