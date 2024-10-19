using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Tourist;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Tourist
{
	public class TouristService : CrudService<TouristEquipment, Equipment>, ITouristService
	{
		private readonly ITouristRepository _touristRepository;

		public TouristService(ITouristRepository repository, IMapper mapper) : base(repository, mapper)
		{
			_touristRepository = repository;
		}

		public Result<bool> UpdateEquipmentToTourist(long touristId, List<long> equipmentIds)
		{
			if (touristId <= 0 || equipmentIds == null || !equipmentIds.Any())
			{
				return Result.Fail<bool>(FailureCode.InvalidArgument).WithError("Invalid tourist ID or equipment IDs.");
			}

			return _touristRepository.UpdateEquipmentToTourist(touristId, equipmentIds);
		}

	}
}
