using Explorer.BuildingBlocks.Core.Domain;
using Markdig;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Explorer.Blog.Core.Domain;

public class BlogPost : Entity
{
    public int UserId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public BlogStatus Status { get; private set; }

    private readonly List<BlogComment> _comments = new List<BlogComment>();
    public virtual IReadOnlyCollection<BlogComment> Comments => _comments.AsReadOnly();

    private readonly List<BlogVote> _votes = new List<BlogVote>();
    public IReadOnlyCollection<BlogVote> Votes => _votes.AsReadOnly();

    private readonly List<BlogImage> _images = new List<BlogImage>();
    public IReadOnlyCollection<BlogImage> Images => _images.AsReadOnly();


    public BlogPost() { }

    public BlogPost(string title, string description, int userId)
    {
        this.Title = title ?? throw new ArgumentNullException(nameof(title));
        this.Description = description ?? throw new ArgumentNullException(nameof(description));
        if (!Enum.IsDefined(typeof(BlogStatus), Status))
        {
            throw new ArgumentException("Invalid Status value.", nameof(Status));
        }

        this.UserId = userId;
        this.Status = BlogStatus.Draft;
        this.CreatedDate = DateTime.UtcNow;
    }

    public void UpdateBlog(string title, string description, int userId)
    {
        if (this.UserId != userId)
            throw new UnauthorizedAccessException("Only the blog creator can update the blog post.");

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty.", nameof(description));

        this.Title = title;
        this.Description = description;
    }

    public void UpdateStatus(BlogStatus newStatus, int currentUserId)
    {
        if (UserId != currentUserId)
        {
            throw new UnauthorizedAccessException("Only the blog creator can change the Status.");
        }
        if (!Enum.IsDefined(typeof(BlogStatus), newStatus))
        {
            throw new ArgumentException("Invalid Status value.", nameof(newStatus));
        }

        Status = newStatus;

    }

    public void Promote(BlogStatus newStatus) {
        if (!Enum.IsDefined(typeof(BlogStatus), newStatus)) {
            throw new ArgumentException("Invalid Status value.", nameof(newStatus));
        }

        Status = newStatus;
    }

    public void UpdateStatusBasedOnVotesAndComments(int upvotes, int downvotes, int commentCount, int currentUserId)
    {
        if (Status == BlogStatus.Published || Status == BlogStatus.Active || Status == BlogStatus.Famous)
        {
            if (upvotes - downvotes < -10)
            {
                Console.WriteLine("Setting Status to Closed");
                Promote(BlogStatus.Closed);
            }
            else if (upvotes > 100 || commentCount > 10)
            {
                Console.WriteLine("Setting Status to Active");
                Promote(BlogStatus.Active);
            }
            else if (upvotes > 500)
            {
                Console.WriteLine("Setting Status to Famous");
                Promote(BlogStatus.Famous);
            }
            else if (upvotes <= 10)
            {
                Console.WriteLine("Setting Status back to Published");
                Promote(BlogStatus.Published);
            }
        }
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
        var comment = _comments.FirstOrDefault(c => c.Id == commentId && c.UserId == userId);
        if (comment == null)
            throw new UnauthorizedAccessException("Comment not found or unauthorized.");
        comment.EditComment(newCommentText);
    }

    public void RemoveComment(long commentId, int userId)
    {
        var comment = _comments.FirstOrDefault(c => c.Id == commentId && c.UserId == userId);
        if (comment == null)
            throw new UnauthorizedAccessException("Comment not found or unauthorized.");
        _comments.Remove(comment);
    }

    public void AddOrUpdateRating(VoteType value, int userId)
    {
        if (Status == BlogStatus.Draft)
            throw new InvalidOperationException("Voting is allowed only for published blogs.");

        //if (this.UserId != UserId)
        //    throw new InvalidOperationException("User cant vote.");

        var existingRating = _votes.FirstOrDefault(r => r.UserId == userId);

        if (existingRating != null)
        {
            _votes.Remove(existingRating);
        }

        var rating = new BlogVote(userId, value);
        _votes.Add(rating);
    }

    public void RemoveRating(int userId)
    {
        if (Status != BlogStatus.Published)
            throw new InvalidOperationException("Voting is allowed only for published blogs.");

        var existingRating = _votes.FirstOrDefault(r => r.UserId == userId);

        if (existingRating != null)
            _votes.Remove(existingRating);
    }

    public int GetUpvoteCount()
    {
        return _votes.Count(v => v.Type == VoteType.Upvote);
    }

    public int GetDownvoteCount()
    {
        return _votes.Count(v => v.Type == VoteType.Downvote);
    }

    public void AddImage(byte[] imageData, string contentType)
    {
        if (Status == BlogStatus.Closed)
            throw new InvalidOperationException("Adding Images is allowed only for blogs that are not closed.");

        var image = new BlogImage(imageData, contentType, (int)this.Id);
        _images.Add(image);
    }

    public void RemoveImage(byte[] imageData, string contentType)
    {
        var image = _images.FirstOrDefault(img => img.Base64Data.SequenceEqual(imageData) && img.ContentType == contentType);

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
        return Markdown.ToHtml(Description);
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
