using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos.TourLifecycle;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Author;

[Collection("Tours")]
public class TourQueryTests : IClassFixture<ToursFixture>
{
    private ToursFixture fixture;

    public TourQueryTests(ToursFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public void Retrieves_all()
    {
        // Arrange
        using var scope = fixture.Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var actionResult = controller.GetByAuthorPaged(-12, 1, 8).Result;
        var result = ((ObjectResult)actionResult)?.Value as List<TourCardDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(2);
    }

    private static TourController CreateController(IServiceScope scope)
    {
        return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
        {
            ControllerContext = ToursFixture.BuildContext("-1")
        };
    }
}