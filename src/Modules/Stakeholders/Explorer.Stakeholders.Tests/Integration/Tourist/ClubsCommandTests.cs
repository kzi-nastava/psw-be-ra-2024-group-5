﻿using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.Tourist
{
    [Collection("Stakeholders")]
    public class ClubsCommandTests : IClassFixture<StakeholdersFixture>
    {
        private StakeholdersFixture fixture;

        public ClubsCommandTests(StakeholdersFixture fixture)
        {
            this.fixture = fixture;
        }

        //[Fact]
        //public void Creates()
        //{
        //    // Arrange
        //    using var scope = fixture.Factory.Services.CreateScope();
        //    var controller = CreateController(scope);
        //    var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        //    var newEntity = new ClubDto
        //    {
        //        Name = "Klub 1",
        //        Description = "Deskripcija 1",
        //        ImageDirectory = "none",
        //        OwnerId = -1
        //    };

        //    // Act
        //    var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ClubDto;

        //    // Assert - Response
        //    result.ShouldNotBeNull();
        //    result.Id.ShouldNotBe(0);
        //    result.Name.ShouldBe(newEntity.Name);
        //    result.OwnerId.ShouldBe(newEntity.OwnerId);

        //    // Assert - Database
        //    var storedEntity = dbContext.Clubs.FirstOrDefault(i => i.Name == newEntity.Name);
        //    storedEntity.ShouldNotBeNull();
        //    storedEntity.Id.ShouldBe(result.Id);
        //}

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ClubDto
            {
                Name = "a",
                ImageDirectory = "none"
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        //[Fact]
        //public void Updates()
        //{
        //    // Arrange
        //    using var scope = fixture.Factory.Services.CreateScope();
        //    var controller = CreateController(scope);
        //    var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
        //    var updatedEntity = new ClubDto
        //    {
        //        Id = -1,
        //        Name = "Klub Update",
        //        Description = "Updatovani klub",
        //        ImageDirectory = "test",
        //        OwnerId = -11
        //    };

        //    // Act
        //    var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as ClubDto;

        //    // Assert - Response
        //    result.ShouldNotBeNull();
        //    result.Id.ShouldBe(-1);
        //    result.Name.ShouldBe(updatedEntity.Name);
        //    result.Description.ShouldBe(updatedEntity.Description);
        //    result.ImageDirectory.ShouldBe(updatedEntity.ImageDirectory);
        //    result.OwnerId.ShouldBe(updatedEntity.OwnerId);

        //    // Assert - Database
        //    var storedEntity = dbContext.Clubs.FirstOrDefault(i => i.Name == "Klub Update");
        //    storedEntity.ShouldNotBeNull();
        //    storedEntity.Description.ShouldBe(updatedEntity.Description);
        //    var oldEntity = dbContext.Clubs.FirstOrDefault(i => i.Name == "Klub 1");
        //    oldEntity.ShouldBeNull();
        //}

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new ClubDto
            {
                Id = -1000,
                Name = "Test"
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();

            // Act
            var result = (OkResult)controller.Delete(-2);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Clubs.FirstOrDefault(i => i.Id == -2);
            storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-1000);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static ClubController CreateController(IServiceScope scope)
        {
            return new ClubController(scope.ServiceProvider.GetRequiredService<IClubService>())
            {
                ControllerContext = StakeholdersFixture.BuildContext("-1")
            };
        }
    }
}
