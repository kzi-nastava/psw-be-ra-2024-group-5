using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogPostDto
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime createdDate { get; set; }
        public BlogStatusDto status { get; set; }
        public List<BlogCommentDto> comments { get; set; } = new();
        public List<BlogImageDto> images { get; set; } = new();
        public List<BlogVoteDto> votes { get; set; } = new();


    }

    public enum BlogStatusDto
    {
        Draft,
        Published,
        Closed,
        Active,
        Famous
    }
}
