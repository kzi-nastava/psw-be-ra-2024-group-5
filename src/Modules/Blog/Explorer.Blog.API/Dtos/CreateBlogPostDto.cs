using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class CreateBlogPostDto
    {
        public int userId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<BlogImageDto> images { get; set; } = new();
    }
}
