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
        public long blogId { get; private set; }
        public long userId {get; private set; }
        public string commentText { get; private set; }
        public DateTime creationTime { get; private set; }
        public DateTime? lastEditedTime { get; private set; }

        public BlogComment() { }
        public BlogComment(long blogId,long userId, string commentTxt)
        {
            if (string.IsNullOrWhiteSpace(commentTxt))
                throw new ArgumentException("Comment text cannot be null or empty.");

            this.blogId = blogId;
            this.userId = userId;
            this.commentText = commentTxt;
            this.creationTime = DateTime.UtcNow;
            this.lastEditedTime = null;
        }

        public void EditComment(string newText)
        {
            commentText = newText ?? throw new ArgumentNullException(nameof(newText));
            lastEditedTime = DateTime.UtcNow;
        }
    }
}
