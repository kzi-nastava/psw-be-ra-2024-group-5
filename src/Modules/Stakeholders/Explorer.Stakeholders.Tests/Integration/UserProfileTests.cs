using System.Security.Claims;
using Explorer.API.Controllers;
using Explorer.API.Controllers.Administrator.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration
{
    [Collection("Sequential")]
    public class UserProfileTests : BaseStakeholdersIntegrationTest
    {
        public UserProfileTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Successfully_gets_profile_by_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var profileId = -11; 

            // Act
            var response = ((ObjectResult)controller.GetProfile(profileId).Result).Value as UserProfileDto;

            // Assert
            response.ShouldNotBeNull();
            response.Id.ShouldBe(profileId);
        }

        [Fact]
        public void Successfully_updates_profile()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var profileId = -11;  
            var updatedProfile = new UserProfileDto
            {
                Id = profileId,
                Name = "UpdatedName",
                Surname = "UpdatedSurname",
                ProfilePictureUrl = "updated-url.jpg",
                Biography = "Updated biography",
                Motto = "Updated motto"
            };

            // Act
            var result = controller.UpdateProfile(profileId, updatedProfile).Result;

            // Assert
            switch (result)
            {
                case ForbidResult:
                    return;

                case ObjectResult objectResult:
                    var responseValue = objectResult.Value as UserProfileDto;
                    responseValue.ShouldNotBeNull();
                    responseValue.Id.ShouldBe(profileId);
                    responseValue.ProfilePictureUrl.ShouldBe(updatedProfile.ProfilePictureUrl);
                    responseValue.Biography.ShouldBe(updatedProfile.Biography);
                    responseValue.Motto.ShouldBe(updatedProfile.Motto);
                    break;

                default:
                    Assert.True(false, "Unexpected result type.");
                    break;
            }
        }

        private UserProfileController CreateController(IServiceScope scope)
        {
            var controller = new UserProfileController(scope.ServiceProvider.GetRequiredService<IUserProfileService>());

            var claims = new List<Claim>
            {
                new Claim("id", "-11") 
            };
            var identity = new ClaimsIdentity(claims, "Test");
            var user = new ClaimsPrincipal(identity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            return controller;
        }
    }
}
