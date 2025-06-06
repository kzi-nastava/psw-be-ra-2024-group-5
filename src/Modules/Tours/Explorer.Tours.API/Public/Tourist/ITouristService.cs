﻿using FluentResults;
using Explorer.Tours.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Tours.API.Public.Tourist
{
	public interface ITouristService
	{
		Result<bool> UpdateEquipmentToTourist(long touristId, List<long> equipmentIds);
		Result<PagedResult<EquipmentDto>> GetTouristEquipment(long touristId);
	}
}
