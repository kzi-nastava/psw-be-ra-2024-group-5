using Explorer.API.Controllers.Administrator.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Administration;

[Collection("Tours")]
public class EquipmentQueryTests : IClassFixture<ToursFixture>
{
    private ToursFixture fixture;

    public EquipmentQueryTests(ToursFixture fixture)
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
        var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<EquipmentDto>;

        // Assert
        result.ShouldNotBeNull();
        result.Results.Count.ShouldBe(3);
        result.TotalCount.ShouldBe(3);
    }

    private static EquipmentController CreateController(IServiceScope scope)
    {
        return new EquipmentController(scope.ServiceProvider.GetRequiredService<IEquipmentService>())
        {
            ControllerContext = ToursFixture.BuildContext("-1")
        };
    }
}