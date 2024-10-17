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
        public long Id { get; private set; }
        public long IdUser {get; private set; }
        public string commentText { get; private set; }
        public DateTime creationTime { get; private set; }
        public DateTime? lastEditedTime { get; private set; }

        public BlogComment() { }
        public BlogComment(long idUser, string commentTxt)
        {
            if (string.IsNullOrWhiteSpace(commentTxt))
                throw new ArgumentException("Comment text cannot be null or empty.");
            
            this.IdUser = idUser;
            this.commentText = commentTxt;
            this.creationTime = DateTime.UtcNow;
            this.lastEditedTime = null;
        }
    }
}
