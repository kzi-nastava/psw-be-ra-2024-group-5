using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogDTO
    {
        public int BblogId { get; set; }  
        public int userId { get; set; }  
        public string title { get; set; }  
        public string description { get; set; }  
        public BlogStatusDto status { get; set; }  
        public List<string> imageUrls { get; set; } = new List<string>();  
    }
    public enum BlogStatusDto
    {
        Draft,
        Published,
        Closed
    }
}
