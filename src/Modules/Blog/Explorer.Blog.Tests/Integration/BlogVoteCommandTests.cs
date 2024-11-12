using Explorer.API.Controllers.Author;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Tests.Integration {

    [Collection("Blog")]
    public class BlogVoteCommandTests : IClassFixture<BlogFixture> {
        private BlogFixture fixture;
        public BlogVoteCommandTests(BlogFixture fixture) {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData(-1, -11, 0, 200)]  // Valid upvote
        [InlineData(-1, -11, 1, 200)]  // Valid downvote
        [InlineData(-99, -11, 0, 400)] // Invalid blog ID
        //[InlineData(-1, -99, 0, 400)]  // Invalid user ID
        public void AddVote(long blogId, int userId, int voteType, int expectedStatusCode) {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var voteDto = new BlogVoteDto {
                userId = userId,
                type = (VoteTypeDto)voteType
            };

            // Act
            var result = (ObjectResult)controller.AddVote(blogId, voteDto).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedStatusCode);
        }

        [Theory]
        [InlineData(-5, -11, 200)]   // Valid vote removal
        [InlineData(-99, -11, 400)]  // Invalid blog ID
        [InlineData(-1, -99, 400)]   // Invalid user ID
        public void RemoveVote(long blogId, int userId, int expectedStatusCode) {
            // Arrange
            using var scope = fixture.Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var voteDto = new BlogVoteDto {
                userId = userId,
                type = VoteTypeDto.Upvote
            };

            // Act
            var r = controller.AddVote(blogId, voteDto);

            var result = (ObjectResult)controller.RemoveVote(blogId, userId).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedStatusCode);
        }
        private static BlogPostController CreateController(IServiceScope scope) {
            return new BlogPostController(scope.ServiceProvider.GetRequiredService<IBlogPostService>()) {
                ControllerContext = BlogFixture.BuildContext("-1")
            };
        }
    }
}
