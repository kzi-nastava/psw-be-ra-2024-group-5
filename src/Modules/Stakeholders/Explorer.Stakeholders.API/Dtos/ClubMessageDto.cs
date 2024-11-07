using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ClubMessageDto
    {
        public long Id { get; set; }
        public long SenderId { get; set; }
        public long ClubId { get; set; }
        public DateTime SentAt { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public Attachment? Attachment { get; set; }
    }
}
