using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Tests.Integration
{
    [Collection("Sequential")]
    public class MembershipCommandTests : BaseBlogIntegrationTest
    {
        public MembershipCommandTests(BlogTestFactory factory) : base(factory){}

        //[Fact]
        //public void Creates()
        //{
        //    // Arrange
        //    using var scope = Factory.Services.CreateScope();
        //    var controller = CreateController(scope);
        //    var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
        //    var newEntity = new ClubMembershipDto
        //    {
        //        ClubId = 1,
        //        UserId = 1
        //    };

        //    // Act
        //    var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as ClubMembershipDto;

        //    // Assert - Response
        //    result.ShouldNotBeNull();
        //    result.Id.ShouldNotBe(0);
        //    result.UserId.ShouldBe(newEntity.UserId);
        //    result.ClubId.ShouldBe(newEntity.ClubId);

        //    // Assert - Database
        //    var storedEntity = dbContext.Memberships.FirstOrDefault(i => i.UserId == newEntity.UserId && i.ClubId == newEntity.ClubId);
        //    storedEntity.ShouldNotBeNull();
        //    storedEntity.Id.ShouldBe(result.Id);
        //}

        //[Fact]
        //public void Create_fails_invalid_data()
        //{
        //    // Arrange
        //    using var scope = Factory.Services.CreateScope();
        //    var controller = CreateController(scope);
        //    var updatedEntity = new ClubMembershipDto
        //    {
        //        ClubId = 1
        //    };

        //    // Act
        //    var result = (ObjectResult)controller.Create(updatedEntity).Result;

        //    // Assert
        //    result.ShouldNotBeNull();
        //    result.StatusCode.ShouldBe(400);
        //}

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            // Act
            var result = (OkResult)controller.Delete(-3);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Memberships.FirstOrDefault(i => i.Id == -3);
            storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-1000);

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        private static ClubMembershipController CreateController(IServiceScope scope)
        {
            return new ClubMembershipController(scope.ServiceProvider.GetRequiredService<IClubMembershipService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

    }
}
