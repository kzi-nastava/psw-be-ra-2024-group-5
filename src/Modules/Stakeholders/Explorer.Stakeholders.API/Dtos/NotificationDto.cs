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
    public List<long> UserIds { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Type { get; set; }
    public long? SenderId { get; set; }
    public long? ProfileMessageId { get; set; }
    public long? ClubMessageId { get; set; }
    public long? ClubId { get; set; }
    public string? Message { get; set; }
    public AttachmentDto? Attachment { get; set; }
    public long? EncounterId { get; set; }
    public List<NotificationReadStatusDto> UserReadStatuses { get; set; }
}


