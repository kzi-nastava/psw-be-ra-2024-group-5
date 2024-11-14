namespace Explorer.Blog.API.Dtos
{
    public class BlogCommentDto
    {
        public long Id { get; set; }
        public long BlogId { get; set; }
        public long UserId { get; set; }
        public string CommentText { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastEditedTime { get; set; }

        public BlogCommentDto()
        {
        }
    }
}
