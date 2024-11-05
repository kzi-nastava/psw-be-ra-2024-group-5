using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ProfileMessage : Message
    {
        public long RecipientId { get; private set; }
        public ProfileMessage(long senderId, long recipientId, string content, Attachment? attachment = null)
            : base(senderId, content, attachment)
        {
            RecipientId = recipientId;
        }
    }
}
