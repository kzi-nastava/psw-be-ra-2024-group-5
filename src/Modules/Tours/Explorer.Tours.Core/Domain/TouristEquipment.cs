using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
	public class TouristEquipment
	{
		public long TouristId { get; init; }

		public long EquipmentId { get; init; }

		public TouristEquipment(long touristId, long equipmentId)
		{
			if (touristId == 0) throw new ArgumentException("Invalid tourist id");
			if (equipmentId == 0) throw new ArgumentException("Invalid equipment id");

			TouristId = touristId;
			EquipmentId = equipmentId;
		}

	}
}
