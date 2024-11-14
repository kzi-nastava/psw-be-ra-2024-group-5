using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Infrastructure.Database;

namespace Explorer.Blog.Infrastructure.Database.Repositories
{
    public class BlogCommentRepository : CrudDatabaseRepository<BlogComment, BlogContext>, IBlogCommentRepository
    {
        private readonly BlogContext _context;
        public BlogCommentRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public List<BlogComment> GetAllByUser(long userId)
        {
            return _context.BlogComments
                           .Where(comment => comment.UserId == userId)
                           .OrderByDescending(comment => comment.CreationTime) //  najnoviji kom. prvo
                           .ToList();
        }

    }
}
