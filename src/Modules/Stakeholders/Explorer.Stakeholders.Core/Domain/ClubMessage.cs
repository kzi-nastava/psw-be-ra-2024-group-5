using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class ClubMessage : Message
    {
        public ClubMessage(long senderId, string content, Attachment? attachment = null)
            : base(senderId, content, attachment){}
    }
}
