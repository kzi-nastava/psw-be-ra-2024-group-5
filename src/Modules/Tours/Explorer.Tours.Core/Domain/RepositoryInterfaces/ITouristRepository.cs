using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces
{
	public interface ITouristRepository : ICrudRepository<Equipment>
	{
		Result<bool> UpdateEquipmentToTourist(long touristId, List<long> equipmentIds);
	}
}
