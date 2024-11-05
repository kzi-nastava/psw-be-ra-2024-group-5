using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.Messages
{
    public class ClubMessage : Message
    {
        public long ClubId { get; private set; }

        public ClubMessage(long senderId, long clubId, string content, Attachment? attachment = null)
            : base(senderId, content, attachment)
        {
            ClubId = clubId;
        }
    }
}
