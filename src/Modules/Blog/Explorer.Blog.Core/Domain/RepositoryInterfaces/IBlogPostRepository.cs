using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Blog.Core.Domain.RepositoryInterfaces
{
    public interface IBlogPostRepository : ICrudRepository<BlogPost>
    {
        BlogPost Get(long id);
        Task<PagedResult<BlogPost>> GetPagedBlogs(int page, int pageSize);
        BlogPost Update(BlogPost aggregateRoot);
        public int GetCommentCountForBlog(long blogId);
    }
}
