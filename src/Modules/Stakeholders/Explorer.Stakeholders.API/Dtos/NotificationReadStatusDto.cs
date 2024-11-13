using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class NotificationReadStatusDto
    {
        public long UserId { get; set; }
        public long NotificationId { get; set; }
        public bool IsRead { get; set; }
    }
}
