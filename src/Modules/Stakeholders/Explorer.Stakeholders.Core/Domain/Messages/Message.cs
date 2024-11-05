using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain.Messages;

public abstract class Message : Entity
{
    public long SenderId { get; private set; }
    public DateTime SentAt { get; private set; } = DateTime.UtcNow;
    public string Content { get; private set; } = string.Empty;
    public Attachment? Attachment { get; private set; }  // Nullable attachment
    public bool IsRead { get; private set; } = false;

    protected Message() { }  // Required by EF Core for materialization

    public Message(long senderId, string content, Attachment? attachment = null)
    {
        SenderId = senderId;
        Content = content;
        Attachment = attachment;
        Validate();  // Ensure valid content length at creation
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }

    public void AddAttachment(Attachment attachment)
    {
        Attachment = attachment;
    }

    public void SetContent(string content)
    {
        if (content.Length > 280)
            throw new InvalidOperationException("Message content cannot exceed 280 characters.");
        Content = content;
    }

    public void Validate()
    {
        if (Content.Length > 280)
            throw new InvalidOperationException("Message content cannot exceed 280 characters.");
    }
}
