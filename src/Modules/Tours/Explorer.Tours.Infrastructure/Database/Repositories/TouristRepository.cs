using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
	public class TouristRepository : CrudDatabaseRepository<Equipment, ToursContext>, ITouristRepository
	{
		private readonly ToursContext _dbContext;

		public TouristRepository(ToursContext dbContext) : base(dbContext)
		{
			_dbContext = dbContext;
		}

		public Result<bool> UpdateEquipmentToTourist(long touristId, List<long> equipmentIds)
		{
			var existingTouristEquipments = _dbContext.TouristEquipment.Where(te => te.TouristId == touristId).ToList();

			//uklanjanje opreme
			foreach (var te in existingTouristEquipments)
			{
				if (!equipmentIds.Contains(te.EquipmentId))
				{
					_dbContext.TouristEquipment.Remove(te);
				}
			}

			//dodavanje opreme
			foreach (var equipmentId in equipmentIds)
			{
				if (!existingTouristEquipments.Any(te => te.EquipmentId == equipmentId))
				{
					var touristEquipment = new TouristEquipment(touristId, equipmentId);
					_dbContext.TouristEquipment.Add(touristEquipment);
				}
			}

			_dbContext.SaveChanges();
			return Result.Ok(true);
		}
	}
}
