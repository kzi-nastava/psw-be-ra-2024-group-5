using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain
{
    public class NotificationReadStatus
    {
        public long UserId { get; set; }   
        public long NotificationId { get; set; }
        public bool IsRead { get; set; }   
    }
}
