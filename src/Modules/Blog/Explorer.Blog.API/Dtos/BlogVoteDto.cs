using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogVoteDto
    {
        public int UserId { get; set; }
        public VoteTypeDto Type { get; set; }
        public DateTime VoteTime { get; set; }
    }

    public enum VoteTypeDto
    {
        Upvote,
        Downvote
    }
}
