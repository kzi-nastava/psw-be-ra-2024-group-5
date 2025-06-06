﻿using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Enum;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Tours")]
    public class FacilityCommandTests : IClassFixture<ToursFixture>
    {
        private ToursFixture fixture;

        public FacilityCommandTests(ToursFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new FacilityDto
            {
                Name = "Parking1",
                Description = "Description",
                Type = FacilityType.Parking,
                Image = "wLrYBfsNR6mHf8+DV5xnUA==",
                Longitude = 0,
                Latitude = 0,
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as FacilityDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newEntity.Name);
            result.Name.ShouldNotBeNull();

            // Assert - Database
            var storedEntity = dbContext.Facilities.FirstOrDefault(i => i.Name == newEntity.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Creates_fails_name_null()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new FacilityDto
            {
                Description = "Description",
                Type = FacilityType.Parking,
                Image = "wLrYBfsNR6mHf8+DV5xnUA==",
                Longitude = 0,
                Latitude = 0,
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new FacilityDto
            {
                Id = -1,
                Name = "Novi parking",
                Description = "Description",
                Type = FacilityType.Parking,
                Image = "wLrYBfsNR6mHf8+DV5xnUA==",
                Longitude = 0,
                Latitude = 0,
            };

            // Act
            var result = ((ObjectResult)controller.Update(newEntity).Result)?.Value as FacilityDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Name.ShouldBe(newEntity.Name);
            result.Name.ShouldNotBeNull();

            // Assert - Database
            var storedEntity = dbContext.Facilities.FirstOrDefault(i => i.Name == newEntity.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
            storedEntity.Latitude.ShouldBe(result.Latitude);
        }

        [Fact]
        public void Updates_not_found()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new FacilityDto
            {
                Id = -535,
                Name = "Novi parking",
                Description = "Description",
                Type = FacilityType.Parking,
                Image = "wLrYBfsNR6mHf8+DV5xnUA==",
                Longitude = 0,
                Latitude = 0,
            };

            // Act
            var result = ((ObjectResult)controller.Update(newEntity).Result);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldNotBe(200);
        }

        private static FacilityController CreateController(IServiceScope scope)
        {
            return new FacilityController(scope.ServiceProvider.GetRequiredService<IFacilityService>())
            {
                ControllerContext = ToursFixture.BuildContext("-1")
            };
        }

    }
}
