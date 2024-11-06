using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Stakeholders.Tests.Integration.Tourist
{
    [Collection("Stakeholders")]
    public class MembershipCommandTests : IClassFixture<StakeholdersFixture>
    {
        private StakeholdersFixture fixture;

        public MembershipCommandTests(StakeholdersFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Add_user_to_club_success()
        {
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            int userId = -11;
            int clubId = -2;

            // Act
            var result = controller.CreateMembership(clubId, userId);

            // Assert
            dbContext.Memberships.Count().ShouldBe(1);
        }


        [Fact]
        public void Remove_user_from_club_success()
        {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<StakeholdersContext>();
            int userId = -1;
            int clubId = -1;

            // Act
            var result = controller.DeleteMembership(clubId, userId);

            // Assert
            dbContext.Memberships.Count().ShouldBe(0);
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
