using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;


namespace Explorer.Stakeholders.API.Public;

public interface IFollowerService
{
    Result AddFollower(long userId, long followedUserId);
    Result RemoveFollower(long userId, long followedUserId);
    Result<PagedResult<UserProfileDto>> GetPagedFollowersByUserId(long userId, int page, int pageSize);
}
