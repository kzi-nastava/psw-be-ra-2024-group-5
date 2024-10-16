using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogImageDTO
    {
        public int Id { get; set; }
        public string base64Data { get; set; }
        public string contentType { get; set; }
    }
}
