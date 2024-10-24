using Explorer.API.Controllers.Tourist;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
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
    public class MembershipQueryTests : BaseStakeholdersIntegrationTest
    {
        public MembershipQueryTests(StakeholdersTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange  
            using var scope = Factory.Services.CreateScope();
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
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
