using Explorer.BuildingBlocks.Core.Domain;
using Markdig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;



namespace Explorer.Blog.Core.Domain
{
    public class Blog : Entity
    {
        public int blogId { get; private set; }
        public int userId { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }
        public DateTime createdDate { get; private set; } 
        public List<string> imageUrls { get; private set; } = new List<string>();
        public BlogStatus status { get; private set; }

        public string RenderedDescription
        {
            get
            {
                return Markdown.ToHtml(description);
            }
        }

        public Blog(string title, string description, BlogStatus status,int userId, List<string> imageUrls = null)
        {
            this.title = title ?? throw new ArgumentNullException(nameof(title));
            this.description = description ?? throw new ArgumentNullException(nameof(description));
            if (!Enum.IsDefined(typeof(BlogStatus), status))
            {
                throw new ArgumentException("Invalid status value.", nameof(status));
            }

            this.status = status;
            this.createdDate = DateTime.UtcNow;

            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be a positive integer.");
            }
            this.userId = userId;

            this.imageUrls = imageUrls ?? new List<string>();
        }

        public void UpdateStatus(BlogStatus newStatus, int currentUserId)
        {
            if (userId != currentUserId)
            {
                throw new UnauthorizedAccessException("Only the blog creator can change the status.");
            }
            status = newStatus;
        }

        public void AddImageUrl(string imageUrl)
        {
            imageUrls.Add(imageUrl);
        }

    }

    public enum BlogStatus
    {
        Draft,
        Published,
        Closed
    }

}
