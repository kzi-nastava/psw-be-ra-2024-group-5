using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos.Messages
{
    public class MessageDto
    {
        public long Id { get; set; }
        public long SenderId { get; set; }
        public string SenderName { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
        public AttachmentDto? Attachment { get; set; }

        public MessageDto() { }

        public MessageDto(long id, long senderId, string senderName, string content,
            DateTime sentAt, bool isRead, AttachmentDto? attachment)
        {
            Id = id;
            SenderId = senderId;
            SenderName = senderName;
            Content = content;
            SentAt = sentAt;
            IsRead = isRead;
            Attachment = attachment;
        }
    }
}
