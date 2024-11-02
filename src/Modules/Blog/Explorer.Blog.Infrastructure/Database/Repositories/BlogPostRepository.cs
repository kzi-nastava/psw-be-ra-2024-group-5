using Explorer.Blog.Core.Domain;
using Explorer.Blog.Core.Domain.RepositoryInterfaces;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Infrastructure.Database.Repositories
{
    public class BlogPostRepository : CrudDatabaseRepository<BlogPost, BlogContext>, IBlogPostRepository
    {
        public BlogPostRepository(BlogContext dbContext) : base(dbContext) { }

        public new BlogPost Get(int id)
        {
            return DbContext.Set<BlogPost>()
                .Include(b => b.comments)
                .Include(b => b.votes)
                .FirstOrDefault(b => b.Id == id)
                ?? throw new KeyNotFoundException("Blog post not found with id: " + id);
        }

        public new BlogPost Update(BlogPost aggregateRoot)
        {
            DbContext.Entry(aggregateRoot).State = EntityState.Modified;
            DbContext.SaveChanges();
            return aggregateRoot;
        }
    }
}
