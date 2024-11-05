namespace Explorer.Blog.API.Dtos
{
    public class BlogCommentDto
    {
        public long id { get; set; }
        public long blogId { get; set; }
        public long userId { get; set; }
        public string commentText { get; set; }
        public DateTime creationTime { get; set; }
        public DateTime? lastEditedTime { get; set; }

        public BlogCommentDto()
        {
        }
    }
}
