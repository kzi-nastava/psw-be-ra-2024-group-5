using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Blog.Core.Domain.RepositoryInterfaces
{
    public interface ICommentRepository : ICrudRepository<BlogComment>
    {
        List<BlogComment> GetAllByUser(long userId);
        List<BlogComment> GetAllByBlogId(long blogId);
    }
}
