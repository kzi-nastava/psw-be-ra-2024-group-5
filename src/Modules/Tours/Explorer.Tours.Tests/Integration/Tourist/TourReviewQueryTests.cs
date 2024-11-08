using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Tourist
{
    [Collection("Tours")]
    public class TourReviewQueryTests : IClassFixture<ToursFixture>
    {
        private ToursFixture fixture;

        public TourReviewQueryTests(ToursFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Retrieves_by_id()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetById(-1).Result)?.Value as TourReviewDto;

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
        }

        [Fact]
        public void Retrieves_by_tour_id()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetByTourId(-1).Result)?.Value as List<TourReviewDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBeGreaterThan(0);
            result.ShouldAllBe(r => r.TourId == -1);
        }

        [Fact]
        public void Retrieves_by_tourist_id()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetByTouristId(-22).Result)?.Value as List<TourReviewDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBeGreaterThan(0);
            result.ShouldAllBe(r => r.TouristId == -22);
        }

        private static TourReviewController CreateController(IServiceScope scope)
        {
            return new TourReviewController(scope.ServiceProvider.GetRequiredService<ITourReviewService>())
            {
                ControllerContext = ToursFixture.BuildContext("-1")
            };
        }
    }
}