using Explorer.API.Controllers.Author;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
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
    public class BlogCommandTests : BaseBlogIntegrationTest
    {
        public BlogCommandTests(BlogTestFactory factory) : base(factory) { }

        [Fact]
        public void CreatesBlog()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var newEntity = new BlogDTO
            {
                userId = 23,
                title = "New Blog",
                description = "This is a test blog.",
                status = BlogStatusDto.Draft,
                createdDate = DateTime.UtcNow,
                imageIds = new List<int>()  // Empty list for this test
            };

            // Act
            var result = ((ObjectResult)controller.CreateBlog(newEntity).Result)?.Value as BlogDTO;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.title.ShouldBe(newEntity.title);

            // Assert - Database
            var storedEntity = dbContext.blogs.FirstOrDefault(i => i.title == newEntity.title);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }



        [Fact]
        public void CreateBlog_Fails_InvalidData()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var newEntity = new BlogDTO
            {
                description = "This is an incomplete blog."  // Missing required title
            };

            // Act
            var result = (ObjectResult)controller.CreateBlog(newEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);  // Expecting a bad request due to missing title
        }

        [Fact]
        public void UpdatesBlogStatus()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var blogId = -3;  // Assuming a test blog with ID 1 exists
            var newStatus = BlogStatusDto.Published; // New status for the blog
            var userId = 123;

            // Act
            var result = ((ObjectResult)controller.UpdateStatus(blogId, newStatus, userId).Result)?.Value as BlogDTO;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(blogId);
            result.status.ShouldBe(newStatus);

            // Assert - Database
            var storedEntity = dbContext.blogs.FirstOrDefault(b => b.Id == blogId);
            storedEntity.ShouldNotBeNull();
            ((BlogStatusDto)storedEntity.status).ShouldBe(BlogStatusDto.Published);
        }


        private static BlogController CreateController(IServiceScope scope)
        {
            return new BlogController(scope.ServiceProvider.GetRequiredService<IBlogService>())
            {
                ControllerContext = BuildContext("1")
            };
        }
    }
}
