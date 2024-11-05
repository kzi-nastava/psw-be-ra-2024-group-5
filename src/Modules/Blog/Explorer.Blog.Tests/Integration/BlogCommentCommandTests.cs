using Explorer.API.Controllers.Author;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Blog.Tests.Integration
{
    [Collection("Sequential")]
    public class BlogCommentCommandTests : BaseBlogIntegrationTest
    {
        public BlogCommentCommandTests(BlogTestFactory factory) : base(factory) { }

        [Fact]
        public void CreatesComment()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var newComment = new BlogCommentDto
            {
                userId = 1,
                commentText = "Ovo je novi testni komentar",
            };

            // Act
            var result = ((ObjectResult)controller.Create(newComment).Result)?.Value as BlogCommentDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.id.ShouldNotBe(0);
            result.commentText.ShouldBe(newComment.commentText);

            // Assert - Database
            var storedComment = dbContext.BlogComments.FirstOrDefault(i => i.commentText == newComment.commentText);
            storedComment.ShouldNotBeNull();
            storedComment.blogId.ShouldBe(result.id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var invalidComment = new BlogCommentDto
            {

            };

            // Act
            var result = (ObjectResult)controller.Create(invalidComment).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }




        [Fact]
        public void UpdatesComment()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            // Kreiraj novi komentar koji ćeš kasnije ažurirati
            var newComment = new BlogCommentDto
            {
                userId = 1,
                commentText = "Originalni komentar"
            };
            var createdResult = ((ObjectResult)controller.Create(newComment).Result)?.Value as BlogCommentDto;
            createdResult.ShouldNotBeNull();
            createdResult.id.ShouldNotBe(0);

            // azurirani kom
            var updatedComment = new BlogCommentDto
            {
                id = createdResult.id, // Koristi ID iz kreiranog komentara
                userId = createdResult.userId,
                commentText = "Ažurirani komentar"
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedComment.id, updatedComment).Result)?.Value as BlogCommentDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.id.ShouldBe(createdResult.id);
            result.commentText.ShouldBe(updatedComment.commentText);

            // Assert - Database
            var storedComment = dbContext.BlogComments.FirstOrDefault(i => i.blogId == updatedComment.id);
            storedComment.ShouldNotBeNull();
            storedComment.commentText.ShouldBe(updatedComment.commentText);
        }



        [Fact]
        public void DeletesComment()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

            // Act
            var result = (OkObjectResult)controller.Delete(-3);
            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedComment = dbContext.BlogComments.FirstOrDefault(i => i.blogId == -3);
            storedComment.ShouldBeNull();
        }




        private static BlogCommentController CreateController(IServiceScope scope)
        {
            return new BlogCommentController(scope.ServiceProvider.GetRequiredService<IBlogCommentService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
