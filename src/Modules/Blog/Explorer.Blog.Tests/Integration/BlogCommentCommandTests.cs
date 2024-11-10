//using Explorer.API.Controllers.Author;
//using Explorer.Blog.API.Dtos;
//using Explorer.Blog.API.Public;
//using Explorer.Blog.Infrastructure.Database;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyInjection;
//using Shouldly;

//namespace Explorer.Blog.Tests.Integration
//{
//    [Collection("Blogs")]
//    public class BlogCommentCommandTests : IClassFixture<BlogFixture>
//    {
//        private BlogFixture fixture;
//        public BlogCommentCommandTests(BlogFixture fixture)
//        {
//            this.fixture = fixture;
//        }

//        [Theory]
//        [InlineData(-11, -2, "Test comment", 200)]  // Valid comment
//        [InlineData(-11, -1, "", 400)]             // Invalid - Empty comment text
//        [InlineData(-11, -2, null, 400)]           // Invalid - Null comment text
//        public void CreatesComment(int userId,int blogId, string commentText, int expectedResponseCode)
//        {
//            // Arrange
//            using var scope = fixture.Factory.Services.CreateScope();
//            var controller = CreateController(scope);

//            var newComment = new BlogCommentDto
//            {
//                userId = userId,
//                commentText = commentText
//            };

//            // Act
//            var result = (ObjectResult)controller.AddComment(blogId, newComment).Result;

//            // Assert
//            result.ShouldNotBeNull();
//            result.StatusCode.ShouldBe(expectedResponseCode);
//        }
        
//        [Theory]
//        [InlineData(-2, -3, -11, "This is a test comment", 200)]
//        [InlineData(-2, -1, -11, "", 400)]  // Invalid - Empty comment text
//        [InlineData(-2, -1, -11, null, 400)] // Invalid - Null comment text
//        public void EditComment(long blogId, long commentId, int userId, string commentText, int expectedStatusCode)
//        {
//            // Arrange
//            using var scope = fixture.Factory.Services.CreateScope();
//            var controller = CreateController(scope);

//            var commentDto = new BlogCommentDto
//            {
//                userId = userId,
//                commentText = commentText
//            };

//            // Act
//            var result = (ObjectResult)controller.EditComment(blogId, commentId, commentDto).Result;

//            // Assert
//            result.ShouldNotBeNull();
//            result.StatusCode.ShouldBe(expectedStatusCode);
//        }

//        [Theory]
//        [InlineData(-11, -4, -4, 200)]   // Successful deletion
//        [InlineData(-21, -4, -1, 400)]   // Unauthorized user
//        [InlineData(-11, 999, -1, 400)] // Non-existent blog ID
//        public void DeletesComment(int userId, int blogId, int commentId, int expectedResponseCode)
//        {
//            // Arrange
//            using var scope = fixture.Factory.Services.CreateScope();
//            var controller = CreateController(scope);

//            // Act
//            var deleteResult = (ObjectResult)controller.RemoveComment(blogId, commentId, userId).Result;

//            // Assert
//            deleteResult.ShouldNotBeNull();
//            deleteResult.StatusCode.ShouldBe(expectedResponseCode);
//        }
//        private static BlogPostController CreateController(IServiceScope scope)
//        {
//            return new BlogPostController(scope.ServiceProvider.GetRequiredService<IBlogPostService>())
//            {
//                ControllerContext = BlogFixture.BuildContext("-1")
//            };
//        }
//    }
//}
