using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Author;

[Collection("Tours")]
public class TourEquipmentCommandTests : IClassFixture<ToursFixture>
{
    private ToursFixture fixture;

    public TourEquipmentCommandTests(ToursFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public void Add_equipment_to_tour_success()
    {
        // Arrange
        using var scope = fixture.Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        List<int> equipmentIds = new List<int> { -1, -2 };
        int tourId = -1;

        // Act
        var result = controller.UpdateTourEquipment(tourId, equipmentIds);

        // Assert
        dbContext.TourEquipment.Count().ShouldBe(2);
    }

    [Fact]
    public void Remove_all_equipment_from_tour_success()
    {
        // Arrange
        using var scope = fixture.Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        List<int> equipmentIds = new List<int>();
        int tourId = -1;

        // Act
        var result = controller.UpdateTourEquipment(tourId, equipmentIds);

        // Assert
        dbContext.TourEquipment.Count().ShouldBe(0);
    }

    private static TourEquipmentController CreateController(IServiceScope scope)
    {
        return new TourEquipmentController(scope.ServiceProvider.GetRequiredService<ITourService>(), scope.ServiceProvider.GetRequiredService<IEquipmentService>())
        {
            ControllerContext = ToursFixture.BuildContext("-1")
        };
    }
}