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

        public List<BlogComment> GetAllByBlogId(long blogPostId)
        {
            Console.WriteLine($"Fetching comments for BlogPostId: {blogPostId}");
            var comments = _context.BlogComments
                                   .Where(comment => comment.BlogPostId == blogPostId)
                                   .OrderByDescending(comment => comment.creationTime)
                                   .ToList();
            Console.WriteLine($"Found {comments.Count} comments");
            return comments;
        }

    }
}
