using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.Tourist
{
    [Collection("Stakeholders")]
    public class MembershipQueryTests : IClassFixture<StakeholdersFixture>
    {
        private StakeholdersFixture fixture;

        public MembershipQueryTests(StakeholdersFixture fixture)
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
            var result = ((ObjectResult)controller.GetAllMemberships().Result)?.Value as List<ClubMembershipDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
        }

        private static ClubMembershipController CreateController(IServiceScope scope)
        {
            return new ClubMembershipController(scope.ServiceProvider.GetRequiredService<IClubService>())
            {
                ControllerContext = StakeholdersFixture.BuildContext("-1")
            };
        }
    }
}
