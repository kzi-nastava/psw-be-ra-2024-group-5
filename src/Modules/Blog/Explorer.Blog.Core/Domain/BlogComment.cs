using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Blog.Core.Domain
{
    public class BlogComment : Entity
    {
        public long Id { get; private set; }
        public long IdUser { get; private set; }
        public long BlogPostId { get; private set; }
        public string commentText { get; private set; }
        public DateTime creationTime { get; private set; }
        public DateTime? lastEditedTime { get; private set; }

        public BlogComment() { }
        public BlogComment(long blogId, long idUser, string commentTxt)
        {
            if (string.IsNullOrWhiteSpace(commentTxt))
                throw new ArgumentException("Comment text cannot be null or empty.");

            this.BlogPostId = blogId;
            this.IdUser = idUser;
            this.commentText = commentTxt;
            this.creationTime = DateTime.UtcNow;
            this.lastEditedTime = null;
        }
    }
}
