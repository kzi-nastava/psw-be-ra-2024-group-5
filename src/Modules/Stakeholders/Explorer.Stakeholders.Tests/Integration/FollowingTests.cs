using Explorer.API.Controllers;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Security.Claims;

namespace Explorer.Stakeholders.Tests.Integration;

[Collection("Stakeholders")]
public class FollowingTests : IClassFixture<StakeholdersFixture>
{
    private StakeholdersFixture fixture;

    public FollowingTests(StakeholdersFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public void successfully_adds_follower()
    {
        // Arrange
        //using var scope = fixture.Factory.Services.CreateScope();
        using var scope = fixture.Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var controller = CreateController(scope);
        var userId = -23;
        var followedUserId = -11;

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
{
            new Claim("id", userId.ToString())
        }, "mock"));

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        // Act
        var result = controller.AddFollower(userId, followedUserId) as OkObjectResult;

        // Asset - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(StatusCodes.Status200OK);

        //Assert - Database
        dbContext.ChangeTracker.Clear();
        var storedFollower = dbContext.Followers.
                                        FirstOrDefault(f => f.UserId == userId && f.FollowedUserId == followedUserId);
        storedFollower.ShouldNotBeNull();
        storedFollower.UserId.ShouldBe(userId);
        storedFollower.FollowedUserId.ShouldBe(followedUserId);
    }

    [Fact]
    public void successfully_removes_follower()
    {
        // Arrange
        using var scope = fixture.Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        var controller = CreateController(scope);
        var userId = -11;
        var followedUserId = -12;

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
{
            new Claim("id", userId.ToString())
        }, "mock"));

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        // Act
        var result = controller.RemoveFollower(userId, followedUserId) as OkObjectResult;

        // Asset - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(StatusCodes.Status200OK);

        //Assert - Database
        dbContext.ChangeTracker.Clear();
        var storedFollower = dbContext.Followers.
                                        FirstOrDefault(f => f.UserId == userId && f.FollowedUserId == followedUserId);
        storedFollower.ShouldBeNull();
    }

    [Fact]
    public void Successfully_gets_all_followers()
    {
        // Arrange
        using var scope = fixture.Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var userId = -11;

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim("id", userId.ToString())
        }, "mock"));

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        // Act
        var response = ((ObjectResult)controller.GetPagedFollowersByUserId(userId, 1, 10).Result).Value as PagedResult<UserProfileDto>;

        // Assert
        response.Results.ShouldNotBeEmpty();
        response.Results.Count.ShouldBe(1);
    }

    private static FollowingController CreateController(IServiceScope scope)
    {
        return new FollowingController(scope.ServiceProvider.GetRequiredService<IFollowingService>());
    }
}
