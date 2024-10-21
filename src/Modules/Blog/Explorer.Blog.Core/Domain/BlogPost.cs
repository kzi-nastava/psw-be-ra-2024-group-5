using Explorer.BuildingBlocks.Core.Domain;
using Markdig;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.Domain;

public class BlogPost : Entity
{
    public int userId { get; private set; }
    public string title { get; private set; }
    public string description { get; private set; }
    public DateTime createdDate { get; private set; } 
    public BlogStatus status { get; private set; }

    public string RenderedDescription
    {
        get
        {
            return Markdown.ToHtml(description);
        }
    }

    public BlogPost() { }

    public BlogPost(string title, string description,int userId)
    {
        this.title = title ?? throw new ArgumentNullException(nameof(title));
        this.description = description ?? throw new ArgumentNullException(nameof(description));
        if (!Enum.IsDefined(typeof(BlogStatus), status))
        {
            throw new ArgumentException("Invalid status value.", nameof(status));
        }

        this.status = BlogStatus.Draft;
        this.createdDate = DateTime.UtcNow;

        if (userId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(userId), "User ID must be a positive integer.");
        }
        this.userId = userId;
    }

    public void UpdateStatus(BlogStatus newStatus, int currentUserId)
    {
        if (userId != currentUserId)
        {
            throw new UnauthorizedAccessException("Only the blog creator can change the status.");
        }
        status = newStatus;
    }

}

public enum BlogStatus
{
    Draft,
    Published,
    Closed
}
