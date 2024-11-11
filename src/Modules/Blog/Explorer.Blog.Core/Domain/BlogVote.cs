using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class BlogVote : ValueObject
    {
        public int userId { get;} 
        public DateTime voteTime { get;}
        public VoteType type { get;}

        private BlogVote() { }

        [JsonConstructor]
        public BlogVote(int userId, VoteType type)
        {
            this.userId = userId;
            this.voteTime = DateTime.UtcNow;

            if (!Enum.IsDefined(typeof(VoteType), type))
                throw new ArgumentException("Invalid status value.", nameof(type));
            this.type = type;
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return userId;
            yield return voteTime;
            yield return type;
        }
    }

    public enum VoteType
    {
        Upvote,
        Downvote
    }
}
