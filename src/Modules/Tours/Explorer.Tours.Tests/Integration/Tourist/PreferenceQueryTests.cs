using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Tourist
{
    [Collection("Sequential")]
    public class PreferenceQueryTests : BaseToursIntegrationTest
    {
        public PreferenceQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(0, 10).Result)?.Value as PagedResult<PreferenceDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBeGreaterThan(0);
            result.TotalCount.ShouldBeGreaterThan(0);
        }

        private static PreferenceController CreateController(IServiceScope scope)
        {
            return new PreferenceController(scope.ServiceProvider.GetRequiredService<IPreferenceService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
