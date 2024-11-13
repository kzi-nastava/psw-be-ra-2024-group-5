using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Blog.Core.Domain.RepositoryInterfaces
{
    public interface IBlogCommentRepository : ICrudRepository<BlogComment>
    {
        List<BlogComment> GetAllByUser(long userId);
    }
}
