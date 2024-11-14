using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Explorer.Blog.Core.Domain
{
    public class BlogComment : Entity
    {
        public long BlogId { get; private set; }
        public long UserId {get; private set; }
        public string CommentText { get; private set; }
        public DateTime CreationTime { get; private set; }
        public DateTime? LastEditedTime { get; private set; }

        public BlogComment() { }
        public BlogComment(long blogId,long userId, string commentTxt)
        {
            if (string.IsNullOrWhiteSpace(commentTxt))
                throw new ArgumentException("Comment text cannot be null or empty.");

            this.BlogId = blogId;
            this.UserId = userId;
            this.CommentText = commentTxt;
            this.CreationTime = DateTime.UtcNow;
            this.LastEditedTime = null;
        }

        public void EditComment(string newText)
        {
            CommentText = newText ?? throw new ArgumentNullException(nameof(newText));
            LastEditedTime = DateTime.UtcNow;
        }
    }
}
