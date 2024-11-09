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
//public class BlogQueryTest : BaseBlogIntegrationTest
//{
//    public BlogQueryTest(BlogTestFactory factory) : base(factory) { }

//    [Fact]
//    public void ConvertsMarkdownToHTML()
//    {
//        // Arrange
//        using var scope = Factory.Services.CreateScope();
//        var controller = CreateController(scope);
//        var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

//        var existingBlog = dbContext.blogs.FirstOrDefault(b => b.Id == -1);
//        existingBlog.ShouldNotBeNull(); 


//        // Act
//        var actionResult = controller.Preview((int)existingBlog.Id);

//        var result = actionResult.Result as OkObjectResult;
//        var htmlResult = result?.Value as string;

//        // Assert - Check if Markdown was correctly converted to HTML
//        htmlResult.ShouldNotBeNull();
//        htmlResult.ShouldBe("<p>This is <strong>bold</strong> and <em>italic</em> text.</p>\n");

//    }

//    [Fact]
//    public void RetrievesAll()
//    {
//        // Arrange
//        using var scope = Factory.Services.CreateScope();
//        var controller = CreateController(scope);

//        // Act
//        var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<BlogDTO>;

//        // Assert
//        result.ShouldNotBeNull();
//        result.Results.Count.ShouldBe(3);
//        result.TotalCount.ShouldBe(3);
//    }


//    private static BlogController CreateController(IServiceScope scope)
//    {
//        return new BlogController(scope.ServiceProvider.GetRequiredService<IBlogService>())
//        {
//            ControllerContext = BuildContext("-1")
//        };
//    }
//}
