namespace Explorer.Blog.API.Dtos
{
    public class BlogCommentDTO
    {
        public long id { get; set; }
        public long userId { get; set; }
        public long blogPostId { get; set; }
        public string commentText { get; set; }
        public DateTime creationTime { get; set; }
        public DateTime? lastEditedTime { get; set; }

        public BlogCommentDTO()
        {
        }
    }
}
