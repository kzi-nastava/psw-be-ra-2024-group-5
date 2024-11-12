using Explorer.BuildingBlocks.Core.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain.RepositoryInterfaces
{
    public interface IBlogPostRepository : ICrudRepository<BlogPost>
    {
        BlogPost GetBlogPost(int id);
        Task<PagedResult<BlogPost>> GetPagedBlogs(int page, int pageSize);
        BlogPost Update(BlogPost aggregateRoot);
    }
}
