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
        public int UserId { get;} 
        public DateTime VoteTime { get;}
        public VoteType Type { get;}

        private BlogVote() { }

        [JsonConstructor]
        public BlogVote(int userId, VoteType type)
        {
            this.UserId = userId;
            this.VoteTime = DateTime.UtcNow;

            if (!Enum.IsDefined(typeof(VoteType), type))
                throw new ArgumentException("Invalid Status value.", nameof(type));
            this.Type = type;
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return VoteTime;
            yield return Type;
        }
    }

    public enum VoteType
    {
        Upvote,
        Downvote
    }
}
