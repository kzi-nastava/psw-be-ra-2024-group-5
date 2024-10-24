using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;


namespace Explorer.Stakeholders.Tests.Integration
{
    [Collection("Sequential")]
    public class AppRatingQueryTests : BaseStakeholdersIntegrationTest
    {
        public AppRatingQueryTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(1, 16).Result)?.Value as PagedResult<AppRatingDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(2);
            result.TotalCount.ShouldBe(2);
        }

        private static AppRatingController CreateController(IServiceScope scope)
        {
            return new AppRatingController(scope.ServiceProvider.GetRequiredService<IAppRatingService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
