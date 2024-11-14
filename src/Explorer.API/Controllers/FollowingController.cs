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
public class FollowingController: BaseApiController
{
    private readonly IFollowingService _followerService;

    public FollowingController(IFollowingService followerService)
    {
        _followerService = followerService;
    }

    [HttpPost("follow/{UserId}/{followedUserId}")]
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

    [HttpDelete("unfollow/{UserId}/{followedUserId}")]
    [Authorize]
    public ActionResult RemoveFollower(long userId, long followedUserId) 
    {
        var currentUserId = GetCurrentUserId();
        if (userId != currentUserId)
        {
            return Forbid();
        }

        var result = _followerService.RemoveFollower(userId, followedUserId);
        return CreateResponse(result);
    }

    [HttpGet("{UserId}")]
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

    [HttpGet("isFollowing/{UserId}/{followedUserId}")]
    [Authorize]
    public ActionResult<bool> IsFollowing(long userId, long followedUserId)
    {
        var result = _followerService.IsAlreadyFollowing(userId, followedUserId);
        return Ok(result);
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
