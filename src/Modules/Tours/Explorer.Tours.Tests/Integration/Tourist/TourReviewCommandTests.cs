using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Dtos.TourLifecycle;
using Explorer.Tours.API.Public;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Tourist
{
    [Collection("Tours")]
    public class TourReviewCommandTests : IClassFixture<ToursFixture>
    {
        private ToursFixture fixture;

        public TourReviewCommandTests(ToursFixture fixture)
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
            var newEntity = new TourReviewDto
            {
                Rating = 4,
                Comment = "Odlicna tura!",
                VisitDate = DateTime.UtcNow.AddDays(-1),
                ReviewDate = DateTime.UtcNow,
                TourId = -1,
                TouristId = -22
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourReviewDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Rating.ShouldBe(newEntity.Rating);
            result.Comment.ShouldBe(newEntity.Comment);
            result.VisitDate.ShouldBe(newEntity.VisitDate);
            result.ReviewDate.ShouldBe(newEntity.ReviewDate, TimeSpan.FromSeconds(1));
            result.TourId.ShouldBe(newEntity.TourId);
            result.TouristId.ShouldBe(newEntity.TouristId);

            // Assert - Database
            var storedEntity = dbContext.TourReviews.FirstOrDefault(i => i.Id == result.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Rating.ShouldBe(result.Rating);
            storedEntity.Comment.ShouldBe(result.Comment);
            storedEntity.VisitDate.ShouldBe(result.VisitDate);
            storedEntity.ReviewDate.ShouldBe(result.ReviewDate, TimeSpan.FromSeconds(1));
            storedEntity.TourId.ShouldBe(result.TourId);
            storedEntity.TouristId.ShouldBe(result.TouristId);
        }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new TourReviewDto
            {
                Id = -1,
                Rating = 5,
                Comment = "Azurirani komentar",
                VisitDate = DateTime.UtcNow.AddDays(-2),
                ReviewDate = DateTime.UtcNow,
                TourId = -1,
                TouristId = -22
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourReviewDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(updatedEntity.Id);
            result.Rating.ShouldBe(updatedEntity.Rating);
            result.Comment.ShouldBe(updatedEntity.Comment);
            result.VisitDate.ShouldBe(updatedEntity.VisitDate);
            result.ReviewDate.ShouldBe(updatedEntity.ReviewDate, TimeSpan.FromSeconds(1));
            result.TourId.ShouldBe(updatedEntity.TourId);
            result.TouristId.ShouldBe(updatedEntity.TouristId);

            // Assert - Database
            var storedEntity = dbContext.TourReviews.FirstOrDefault(i => i.Id == updatedEntity.Id);
            storedEntity.ShouldNotBeNull();
            storedEntity.Rating.ShouldBe(result.Rating);
            storedEntity.Comment.ShouldBe(result.Comment);
            storedEntity.VisitDate.ShouldBe(result.VisitDate);
            storedEntity.ReviewDate.ShouldBe(result.ReviewDate, TimeSpan.FromSeconds(1));
            storedEntity.TourId.ShouldBe(result.TourId);
            storedEntity.TouristId.ShouldBe(result.TouristId);
        }

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (OkResult)controller.Delete(-2);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedEntity = dbContext.TourReviews.FirstOrDefault(i => i.Id == -2);
            storedEntity.ShouldBeNull();
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