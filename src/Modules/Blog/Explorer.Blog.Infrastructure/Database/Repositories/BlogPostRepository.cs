using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Blog.Infrastructure.Database.Repositories
{
    public class BlogPostRepository : CrudDatabaseRepository<BlogPost, BlogContext>, IBlogPostRepository
    {
        public BlogPostRepository(BlogContext dbContext) : base(dbContext) { }

        public new BlogPost Get(long id)
        {
            return DbContext.Set<BlogPost>()
                .Include(b => b.Comments)
                .FirstOrDefault(b => b.Id == id)
                ?? throw new KeyNotFoundException("Blog post not found with id: " + id);
        }

        public async Task<PagedResult<BlogPost>> GetPagedBlogs(int page, int pageSize)
        {
            var source = DbContext.Set<BlogPost>()
                .Include(b => b.Comments)
                .AsQueryable();

            var totalCount = await source.CountAsync();

            if (pageSize > 0 && page > 0)
            {
                source = source.OrderByDescending(b => b.Id)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize);
            }

            var items = await source.ToListAsync();

            return new PagedResult<BlogPost>(items, totalCount);
        }

        public new BlogPost Update(BlogPost aggregateRoot)
        {
            DbContext.Entry(aggregateRoot).State = EntityState.Modified;
            DbContext.SaveChanges();
            return aggregateRoot;
        }

        public int GetCommentCountForBlog(long blogId)
        {
            return DbContext.BlogComments.Count(c => c.BlogId == blogId);
        }




    }
}
