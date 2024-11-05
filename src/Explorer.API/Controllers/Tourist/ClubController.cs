using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/club")]
    public class ClubController : BaseApiController
    {
        private readonly IClubService _clubService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ClubDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _clubService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

            
        [HttpPost]
        public ActionResult<ClubDto> Create([FromBody] ClubDto club)
        {
            var result = _clubService.Create(club);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ClubDto> Update([FromBody] ClubDto club)
        {
            var result = _clubService.Update(club);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _clubService.Delete(id);
            return CreateResponse(result);
        }

        [HttpPost("messages")]
        public ActionResult AddMessageToClub(long clubId, ClubMessageDto messageDto, long userId)
        {
            var result = _clubService.AddMessageToClub(clubId, messageDto, userId);
            return CreateResponse(result);
        }

        [HttpDelete("messages")]
        public ActionResult RemoveMessageFromClub(long clubId, long messageId, long userId)
        {
            var result = _clubService.RemoveMessageFromClub(clubId, messageId, userId);
            return CreateResponse(result);
        }

        [HttpPut("messages")]
        public ActionResult UpdateMessageInClub(long clubId, long messageId, long userId, [FromBody] string newContent)
        {
            var result = _clubService.UpdateMessageInClub(clubId, messageId, userId, newContent);
            return CreateResponse(result); 
        }

        [HttpGet("messages")]
        public ActionResult<PagedResult<ClubMessageDto>> GetPagedMessagesByClubId(long clubId, int page, int pageSize)
        {
            var result = _clubService.GetPagedMessagesByClubId(clubId, page, pageSize);
            return CreateResponse(result);
        }
    }
}
