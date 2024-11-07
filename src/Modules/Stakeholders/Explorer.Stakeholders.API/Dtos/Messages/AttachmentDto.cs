using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos.Messages
{
    public class AttachmentDto
    {
        public long? Id { get; set; }
        public long ResourceId { get; set; }
        public int ResourceType { get; set; }

        public AttachmentDto() { }

        public AttachmentDto(long resourceId, int resourceType)
        {
            ResourceId = resourceId;
            ResourceType = resourceType;
        }

        public AttachmentDto(long id, long resourceId, int resourceType)
        {
            Id = id;
            ResourceId = resourceId;
            ResourceType = resourceType;
        }
    }
}
