using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.API.Dtos.Messages;

namespace Explorer.Stakeholders.API.Dtos;

public class NotificationDto
{
    public long Id { get; set; }
    public long UserId { get; private set; }
    public string Content { get; private set; }
    public bool IsRead { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int Type { get; private set; }
    public long? SenderId { get; private set; }
    public long? clubId { get; private set; }
    public string? Message { get; private set; }
    public AttachmentDto? Attachment { get; private set; }
}


