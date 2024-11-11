using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Dtos
{
    public class BlogVoteDto
    {
        public int userId { get; set; }
        public VoteTypeDto type { get; set; }
        public DateTime voteTime { get; set; }
    }

    public enum VoteTypeDto
    {
        Upvote,
        Downvote
    }
}
