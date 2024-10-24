using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Infrastructure.Database;

namespace Explorer.Blog.Infrastructure.Database.Repositories
{
    public class CommentRepository : CrudDatabaseRepository<BlogComment, BlogContext>, ICommentRepository
    {
        private readonly BlogContext _context;
        public CommentRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public List<BlogComment> GetAllByUser(long userId)
        {
            return _context.BlogComments
                           .Where(comment => comment.IdUser == userId)
                           .OrderByDescending(comment => comment.creationTime) //  najnoviji kom. prvo
                           .ToList();
        }

    }
}
