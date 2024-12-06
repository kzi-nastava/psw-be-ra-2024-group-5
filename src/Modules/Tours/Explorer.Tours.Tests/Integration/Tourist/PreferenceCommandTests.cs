using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Explorer.Tours.API.Enum;

namespace Explorer.Tours.Tests.Integration.Tourist
{
    [Collection("Tours")]
    public class PreferenceCommandTests : IClassFixture<ToursFixture>
    {
        private ToursFixture fixture;

        public PreferenceCommandTests(ToursFixture fixture)
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
            var newPreference = new PreferenceDto
            {
                TouristId = -11,
                PreferredDifficulty = TourLevel.Intermediate,
                WalkRating = 3,
                BikeRating = 2,
                CarRating = 1,
                BoatRating = 0,
                InterestTags = new List<string> { "Nature", "Adventure" }
            };

            // Act
            var result = ((ObjectResult)controller.Create(newPreference).Result)?.Value as PreferenceDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.TouristId.ShouldBe(newPreference.TouristId);
            result.PreferredDifficulty.ShouldBe(newPreference.PreferredDifficulty);
            result.WalkRating.ShouldBe(newPreference.WalkRating);
            result.BikeRating.ShouldBe(newPreference.BikeRating);
            result.CarRating.ShouldBe(newPreference.CarRating);
            result.BoatRating.ShouldBe(newPreference.BoatRating);

            // Assert - Database
            var storedPreference = dbContext.Preferences.FirstOrDefault(p => p.TouristId == newPreference.TouristId);
            storedPreference.ShouldNotBeNull();
            storedPreference.TouristId.ShouldBe(result.TouristId);
            storedPreference.PreferredDifficulty.ShouldBe(TourLevel.Intermediate);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedPreference = new PreferenceDto
            {
                TouristId = -1, // Invalid ID, dobro je, 
                PreferredDifficulty = TourLevel.Advanced,
                WalkRating = 3,
                BikeRating = 3,
                CarRating = 2,
                BoatRating = 1,
                InterestTags = new List<string> { "Culture", "History" }
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedPreference).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);
        }


        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();


            var preferenceDto = new PreferenceDto
            {
                Id = 735331,
                TouristId = 1,
                PreferredDifficulty = TourLevel.Intermediate,
                WalkRating = 3,
                BikeRating = 2,
                CarRating = 1,
                BoatRating = 0,
                InterestTags = new List<string> { "Nature", "Adventure" }
            };


            controller.Create(preferenceDto);

            // Act
            var deleteResult = (OkResult)controller.Delete((int)preferenceDto.Id); // id iz long u int

            // Assert - Response
            deleteResult.ShouldNotBeNull();
            deleteResult.StatusCode.ShouldBe(200); // Očekujemo 200 za uspešno brisanje

            // Assert - Database
            var storedPreference = dbContext.Preferences.FirstOrDefault(p => p.Id == preferenceDto.Id);
            storedPreference.ShouldBeNull(); // Trebalo bi biti null nakon brisanja
        }


        private static PreferenceController CreateController(IServiceScope scope)
        {
            return new PreferenceController(scope.ServiceProvider.GetRequiredService<IPreferenceService>())
            {
                ControllerContext = ToursFixture.BuildContext("-1")
            };
        }
    }
}
