using Explorer.API.Controllers.Author;
using Explorer.Blog.API.Public;
using Explorer.Blog.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Linq;

namespace Explorer.Blog.Tests.Integration;

[Collection("Sequential")]
public class BlogMarkdownTests : BaseBlogIntegrationTest
{
    public BlogMarkdownTests(BlogTestFactory factory) : base(factory) { }

    [Fact]
    public void Converts_Markdown_To_HTML_With_Existing_BlogId()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

        // Retrieve a blog with Markdown in the description from the existing data
        var existingBlog = dbContext.blogs.FirstOrDefault(b => b.Id == -1);
        existingBlog.ShouldNotBeNull(); // Ensure there is an existing blog with Markdown

        // Act
        var actionResult = controller.Preview((int)existingBlog.Id);

        var result = actionResult.Result as OkObjectResult; // If it's an OkObjectResult, cast to access the Value
        var htmlResult = result?.Value as string; // Get the string from the Value

        // Assert - Check if Markdown was correctly converted to HTML
        htmlResult.ShouldNotBeNull();
        htmlResult.ShouldBe("<p>This is <strong>bold</strong> and <em>italic</em> text.</p>\n");

    }

    private static BlogController CreateController(IServiceScope scope)
    {
        return new BlogController(scope.ServiceProvider.GetRequiredService<IBlogService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}
