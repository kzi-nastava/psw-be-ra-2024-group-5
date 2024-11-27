using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos {
    public class EncounterExecutionRequestDto {
        public long EncounterId { get; set; }
        public long UserId { get; set; }
        public Location Location { get; set; }
        public EncounterExecutionRequestDto() { }
    }
}
