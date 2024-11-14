namespace Explorer.Stakeholders.API.Dtos.Messages;

public class SendMessageDto
{
    public long SenderId { get; set; }
    public long RecipientId { get; set; }
    public string Content { get; set; }
    public AttachmentDto? Attachment { get; set; }
}