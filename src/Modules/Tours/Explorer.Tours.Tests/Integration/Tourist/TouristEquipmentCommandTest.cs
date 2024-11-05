using Explorer.API.Controllers.Tourist;
using Explorer.Tours.API.Public.Tourist;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Tourist
{
    [Collection("Tours")]
    public class TouristEquipmentCommandTest : IClassFixture<ToursFixture>
    {
        private ToursFixture fixture;

        public TouristEquipmentCommandTest(ToursFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Update_equipment_to_tourist()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            List<int> equipmentIds = new List<int> { -1, -2, -3 };
            int touristId = -21;

            // Act
            var result = controller.UpdateTouristEquipment(touristId, equipmentIds);

            // Assert
            dbContext.TouristEquipment.Count().ShouldBe(4);
        }

        private static TouristController CreateController(IServiceScope scope)
        {
            return new TouristController(scope.ServiceProvider.GetRequiredService<ITouristService>())
            {
                ControllerContext = ToursFixture.BuildContext("-1")
            };
        }

    }
}
