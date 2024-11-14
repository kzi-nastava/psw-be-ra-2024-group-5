using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogPostDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public BlogStatusDto Status { get; set; }
        public List<BlogCommentDto> Comments { get; set; } = new();
        public List<BlogImageDto> Images { get; set; } = new();
        public List<BlogVoteDto> Votes { get; set; } = new();
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
