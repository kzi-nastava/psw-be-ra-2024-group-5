//using Explorer.API.Controllers.Author;
//using Explorer.Blog.API.Dtos;
//using Explorer.Blog.API.Public;
//using Explorer.Blog.Infrastructure.Database;
//using Explorer.BuildingBlocks.Core.UseCases;
//using Explorer.Tours.API.Dtos;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.DependencyInjection;
//using Shouldly;
//using System.Linq;

//namespace Explorer.Blog.Tests.Integration;

//[Collection("Sequential")]
//public class BlogQueryTest : IClassFixture<BlogFixture>
//{
//    private BlogFixture fixture;
//    public BlogQueryTest(BlogFixture fixture)
//    {
//        this.fixture = fixture;
//    }

//    [Fact]
//    public void RetrievesAll()
//    {
//        // Arrange
//        using var scope = fixture.Factory.Services.CreateScope();
//        var controller = CreateController(scope);

//        // Act
//        var result = controller.GetAllBlogPosts(0, 0).Result;

//        // Assert
//        result.ShouldNotBeNull();
//        result.ShouldBe(4);
//        result.TotalCount.ShouldBe(4);
//    }


//    private static BlogPostController CreateController(IServiceScope scope)
//    {
//        return new BlogPostController(scope.ServiceProvider.GetRequiredService<IBlogPostService>())
//        {
//            ControllerContext = BlogFixture.BuildContext("-1")
//        };
//    }
//}
