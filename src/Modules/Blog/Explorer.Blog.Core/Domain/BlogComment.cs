using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain
{
    public class BlogComment : Entity
    {
        public int id { get; private set; }
        public string username { get; private set; }
        public string commentText { get; private set; }
        public DateTime creationTime { get; private set; }
        public DateTime? lastEditedTime { get; private set; }

        public BlogComment() { }
        public BlogComment(string userName, string commentTxt)
        {
            if (string.IsNullOrWhiteSpace(commentTxt))
                throw new ArgumentException("Comment text cannot be null or empty.");

            this.username = userName;
            this.commentText = commentTxt;
            this.creationTime = DateTime.Now;
            this.lastEditedTime = null;
        }
    }
}
