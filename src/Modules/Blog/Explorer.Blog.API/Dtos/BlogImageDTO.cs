using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogImageDto
    {
        public int BlogId { get; set; }
        public string Base64Data { get; set; }
        public string ContentType { get; set; }
        
    }
}
