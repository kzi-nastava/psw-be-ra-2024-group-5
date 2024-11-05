using Explorer.BuildingBlocks.Tests;

namespace Explorer.Blog.Tests;

public class BlogFixture : BaseWebIntegrationTest<BlogTestFactory>, IDisposable
{
    public BlogFixture() : base(new BlogTestFactory()) { }

    public void Dispose()
    {
        Factory.Dispose();
    }
}
