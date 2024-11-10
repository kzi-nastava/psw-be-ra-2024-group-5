using Explorer.BuildingBlocks.Core.Domain;
using Markdig;

namespace Explorer.Blog.Core.Domain;

public class BlogPost : Entity
{
    public int userId { get; private set; }
    public string title { get; private set; }
    public string description { get; private set; }
    public DateTime createdDate { get; private set; } 
    public BlogStatus status { get; private set; }

    private readonly List<BlogComment> _comments = new List<BlogComment>();
    public IReadOnlyCollection<BlogComment> comments => _comments.AsReadOnly();

    private readonly List<BlogVote> _votes = new List<BlogVote>();
    public IReadOnlyCollection<BlogVote> votes => _votes.AsReadOnly();

    private readonly List<BlogImage> _images = new List<BlogImage>();
    public IReadOnlyCollection<BlogImage> images => _images.AsReadOnly();


    public BlogPost() { }

    public BlogPost(string title, string description,int userId)
    {
        this.title = title ?? throw new ArgumentNullException(nameof(title));
        this.description = description ?? throw new ArgumentNullException(nameof(description));
        if (!Enum.IsDefined(typeof(BlogStatus), status))
        {
            throw new ArgumentException("Invalid status value.", nameof(status));
        }

        this.userId = userId;
        this.status = BlogStatus.Draft;
        this.createdDate = DateTime.UtcNow;
    }

    public void UpdateBlog(string title, string description, int userId)
    {
        if (this.userId != userId)
            throw new UnauthorizedAccessException("Only the blog creator can update the blog post.");

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));

        this.title = title;
        this.description = description;
    }
    public void UpdateStatus(BlogStatus newStatus, int currentUserId)
    {
        if (userId != currentUserId)
        {
            throw new UnauthorizedAccessException("Only the blog creator can change the status.");
        }
        if (!Enum.IsDefined(typeof(BlogStatus), newStatus))
        {
            throw new ArgumentException("Invalid status value.", nameof(newStatus));
        }

        status = newStatus;
    }

    public void AddComment(long id, string text, int userId)
    {
        var comment = new BlogComment(id, userId, text);
        _comments.Add(comment);
    }

    public IReadOnlyCollection<BlogComment> GetAllComments()
    {
        return _comments.AsReadOnly();
    }

    public void EditComment(long commentId, string newCommentText, int userId)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == commentId && c.userId == userId);
        if (comment == null)
            throw new UnauthorizedAccessException("Comment not found or unauthorized.");
        comment.EditComment(newCommentText);
    }

    public void RemoveComment(long commentId, int userId)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == commentId && c.userId == userId);
        if (comment == null)
            throw new UnauthorizedAccessException("Comment not found or unauthorized.");
        _comments.Remove(comment);
    }

    public void AddOrUpdateRating(VoteType value, int userId)
    {
        if (status != BlogStatus.Published)
            throw new InvalidOperationException("Voting is allowed only for published blogs.");

        //if (this.userId != userId)
        //    throw new InvalidOperationException("User cant vote.");

        var existingRating = _votes.FirstOrDefault(r => r.userId == userId);

        if (existingRating != null)
        {
            _votes.Remove(existingRating);
        }

        var rating = new BlogVote(userId, value);
        _votes.Add(rating);
    }

    public void RemoveRating(int userId)
    {
        if (status != BlogStatus.Published)
            throw new InvalidOperationException("Voting is allowed only for published blogs.");
        
        var existingRating = _votes.FirstOrDefault(r => r.userId == userId);

        if (existingRating != null)
            _votes.Remove(existingRating);
    }

    public int GetUpvoteCount()
    {
        return _votes.Count(v => v.type == VoteType.Upvote);
    }

    public int GetDownvoteCount()
    {
        return _votes.Count(v => v.type == VoteType.Downvote);
    }

    public void AddImage(byte[] imageData, string contentType)
    {
        if (status == BlogStatus.Closed)
            throw new InvalidOperationException("Adding images is allowed only for blogs that are not closed.");

        var image = new BlogImage(imageData, contentType, (int)this.Id);
        _images.Add(image);
    }

    public void RemoveImage(byte[] imageData, string contentType)
    {
        var image = _images.FirstOrDefault(img => img.base64Data.SequenceEqual(imageData) && img.contentType == contentType);

        if (image == null)
            throw new InvalidOperationException("Image not found.");

        _images.Remove(image);
    }

    public IReadOnlyCollection<BlogImage> GetAllImages()
    {
        return _images.AsReadOnly();
    }


    public string RenderDescriptionToMarkdown()
    {
        return Markdown.ToHtml(description);
    }
}

public enum BlogStatus
{
    Draft,
    Published,
    Closed,
    Active,
    Famous
}
