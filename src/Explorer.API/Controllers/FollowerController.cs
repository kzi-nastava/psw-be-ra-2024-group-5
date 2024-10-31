using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers;

[Route("api/administration/followers")]
public class FollowerController: BaseApiController
{
    private readonly IFollowerService _followerService;

    public FollowerController(IFollowerService followerService)
    {
        _followerService = followerService;
    }

    [HttpPost("follow/{userId}/{followedUserId}")]
    [Authorize]
    public ActionResult AddFollower(long userId, long followedUserId)
    {
        var currentUserId = GetCurrentUserId();
        if (userId != currentUserId)
        {
            return Forbid();
        }

        var result = _followerService.AddFollower(userId, followedUserId);
        return CreateResponse(result);
    }

    [HttpDelete("unfollow/{userId}/{followedUserId}")]
    [Authorize]
    public ActionResult DeleteFollower(long userId, long followedUserId) 
    {
        var currentUserId = GetCurrentUserId();
        if (userId != currentUserId)
        {
            return Forbid();
        }

        var result = _followerService.RemoveFollower(userId, followedUserId);
        return CreateResponse(result);
    }

    [HttpGet("{userId}")]
    [Authorize]
    public ActionResult<PagedResult<UserProfileDto>> GetPagedFollowersByUserId(long userId, int page, int pageSize) 
    {
        var currentUserId = GetCurrentUserId();
        if (userId != currentUserId)
        {
            return Forbid();
        }

        var result = _followerService.GetPagedFollowersByUserId(userId, page, pageSize);
        return CreateResponse(result);
    }

    private long GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst("id")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        return long.Parse(userIdClaim);
    }
}
