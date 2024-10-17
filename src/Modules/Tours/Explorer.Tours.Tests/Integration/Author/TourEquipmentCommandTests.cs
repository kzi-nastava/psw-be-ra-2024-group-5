using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Author;

[Collection("Sequential")]
public class TourEquipmentCommandTests : BaseToursIntegrationTest
{
    public TourEquipmentCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Add_equipment_to_tour_success()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        List<int> equipmentIds = new List<int> { -1, -2 };
        int tourId = 1;

        // Act
        var result = controller.UpdateTourEquipment(tourId, equipmentIds);
        
        // Assert
        dbContext.TourEquipment.Count().ShouldBe(2);
    }
    
    private static TourEquipmentController CreateController(IServiceScope scope) {
        return new TourEquipmentController(scope.ServiceProvider.GetRequiredService<ITourService>()) {
            ControllerContext = BuildContext("-1")
        };
    } 
}