using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain;

public class Message : Entity
{
    public long SenderId { get; private set; }
    public long RecipientId { get; private set; }
    public DateTime SentAt { get; private set; } = DateTime.UtcNow;
    public string Content { get; private set; } = string.Empty;
    public Attachment? Attachment { get; private set; }
    public bool IsRead { get; private set; } = false;

    public Message(long senderId, long recipientId, string content, Attachment? attachment = null)
    {
        SenderId = senderId;
        RecipientId = recipientId;
        Content = content;
        Attachment = attachment;
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }

    public void AddAttachment(Attachment attachment)
    {
        Attachment = attachment;
    }

    public void Validate()
    {
        if (Content.Length > 280)
            throw new InvalidOperationException("Message content cannot exceed 280 characters.");
    }
}
