using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.Tourist
{
    [Collection("Stakeholders")]
    public class ClubQueryTests : IClassFixture<StakeholdersFixture>
    {
        private StakeholdersFixture fixture;

        public ClubQueryTests(StakeholdersFixture fixture)
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
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<ClubDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(2);
            result.TotalCount.ShouldBe(2);
        }

        private static ClubController CreateController(IServiceScope scope)
        {
            return new ClubController(scope.ServiceProvider.GetRequiredService<IClubService>())
            {
                ControllerContext = StakeholdersFixture.BuildContext("-1")
            };
        }
    }
}
