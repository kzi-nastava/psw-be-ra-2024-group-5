using Explorer.Encounters.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.API.Dtos {
    public class HiddenLocationEncounterDto : EncounterDto {
        public LocationDto ImageLocation { get; set; }
        public string Image { get; set; }
        public HiddenLocationEncounterDto() {
            Type = EncounterType.Locaion;
        }

    }
}
