using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Tests.Integration.Tourist
{
    [Collection("Sequential")]
    public class MembershipCommandTests : BaseStakeholdersIntegrationTest
    {
        public MembershipCommandTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Add_user_to_club_success()
        {
            using var scope = Factory.Services.CreateScope();
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
            using var scope = Factory.Services.CreateScope();
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
                ControllerContext = BuildContext("-1")
            };
        }

    }
}
