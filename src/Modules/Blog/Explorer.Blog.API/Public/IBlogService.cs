using Explorer.Blog.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Public
{
    public interface IBlogService
    {
        BlogDTO CreateBlog(BlogDTO blogDTO);
        BlogDTO UpdateBlogStatus(int blogId, BlogStatusDto newStatus, int userId);
        string PreviewBlogDescription(string description);
    }
}
