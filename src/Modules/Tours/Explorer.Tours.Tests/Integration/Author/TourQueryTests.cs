using Explorer.API.Controllers.Author;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Author;

[Collection("Sequential")]
public class TourQueryTests : BaseToursIntegrationTest {
    public TourQueryTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Retrieves_all() {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);

        // Act
        var result = ((ObjectResult)controller.GetByAuthor(-12).Result)?.Value as PagedResult<TourDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(2);
        result.TotalCount.ShouldBe(2);
    }

    private static TourController CreateController(IServiceScope scope) {
        return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>()) {
            ControllerContext = BuildContext("-1")
        };
    }
}