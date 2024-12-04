using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Tourist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
	[Authorize(Policy = "touristPolicy")]
	[Route("api/tourist/{touristId:int}/equipment")]
	public class TouristController : BaseApiController
	{
		private readonly ITouristService _touristService;
		public TouristController(ITouristService touristService)
		{
			_touristService = touristService;
		}

		[HttpPut]
		public ActionResult UpdateTouristEquipment(int touristId, [FromBody] List<int> equipmentIds)
		{
			long id = (long)touristId;
			List<long> ids = equipmentIds.Select(id => (long)id).ToList();
			var result = _touristService.UpdateEquipmentToTourist(id,ids);

			return CreateResponse(result);
		}

		[HttpGet]
		public ActionResult<PagedResult<EquipmentDto>> GetTouristEqupment(int touristId) { 
			var result = _touristService.GetTouristEquipment(touristId);
			return CreateResponse<PagedResult<EquipmentDto>>(result);
		}

	}
}
