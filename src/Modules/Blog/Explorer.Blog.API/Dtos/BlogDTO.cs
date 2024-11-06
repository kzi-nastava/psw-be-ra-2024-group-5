namespace Explorer.Blog.API.Dtos
{
    public class BlogDTO
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public BlogStatusDto status { get; set; }
        public List<BlogImageDTO> imageData { get; set; } = new List<BlogImageDTO>();
        public DateTime createdDate { get; set; }
    }
    public enum BlogStatusDto
    {
        Draft,
        Published,
        Closed
    }
}
