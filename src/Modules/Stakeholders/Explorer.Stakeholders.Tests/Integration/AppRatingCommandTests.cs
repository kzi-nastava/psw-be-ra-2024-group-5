using Explorer.Stakeholders.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Explorer.Stakeholders.API.Public;
using Shouldly;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.Tests;
using Explorer.API.Controllers.Tourist;

namespace Explorer.Tours.Tests.Integration
{
    [Collection("Sequential")]
    public class AppRatingCommandTests : BaseStakeholdersIntegrationTest
    {
        public AppRatingCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var newEntity = new AppRatingDto
            {
                Grade = 3,
                Comment = "Excellent",
                UserId = 1,
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as AppRatingDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Grade.ShouldBe(newEntity.Grade);
            result.Comment.ShouldBe(newEntity.Comment);

            // Assert - Database
            var storedEntity = dbContext.AppRating.FirstOrDefault(i => i.Comment == newEntity.Comment && i.UserId == newEntity.UserId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }


        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new AppRatingDto
            {
                Grade = 0,
                Comment = "Test"
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Deletes()   
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = (OkResult)controller.Delete(-2);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.AppRating.FirstOrDefault(i => i.Id == 2);
            storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-1000);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }


        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            var updatedEntity = new AppRatingDto
            {
                Id = -1,
                Grade = 2,
                Comment = "Very bad experience",
                UserId = 1,
                TimeStamp = DateTime.UtcNow
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as AppRatingDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Grade.ShouldBe(updatedEntity.Grade);
            result.Comment.ShouldBe(updatedEntity.Comment);


            // Assert - Database
            var storedEntity = dbContext.AppRating.FirstOrDefault(i => i.Comment == "Very bad experience");
            storedEntity.ShouldNotBeNull();
            storedEntity.Grade.ShouldBe(updatedEntity.Grade);
            storedEntity.UserId.ShouldBe(updatedEntity.UserId);
            var oldEntity = dbContext.AppRating.FirstOrDefault(i => i.Comment == "Okay");
            oldEntity.ShouldBeNull();
        }


        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new AppRatingDto
            {
                Id = -1000,
                Grade = 4,
                Comment = "Test",
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }


        private static AppRatingController CreateController(IServiceScope scope)
        {
            return new AppRatingController(scope.ServiceProvider.GetRequiredService<IAppRatingService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

    }
}