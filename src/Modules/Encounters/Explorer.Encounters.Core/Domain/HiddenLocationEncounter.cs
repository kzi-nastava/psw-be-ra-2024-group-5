using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain {
    public class HiddenLocationEncounter : Encounter {
        public Location ImageLocation { get; set; }
        public byte[] Image { get; set; }

        public HiddenLocationEncounter() { }

        public bool IsCloseToImageLocation(Location location) {
            return GeoCalculator.IsClose(location, ImageLocation, 20);
        }
    }
}
