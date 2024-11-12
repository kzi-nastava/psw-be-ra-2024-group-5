using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Blog.Core.Domain.RepositoryInterfaces
{
    public interface IBlogPostRepository : ICrudRepository<BlogPost>
    {
        BlogPost GetBlogPost(int id);
        Task<PagedResult<BlogPost>> GetPagedBlogs(int page, int pageSize);
        BlogPost Update(BlogPost aggregateRoot);
        public int GetCommentCountForBlog(long blogId);
    }
}
