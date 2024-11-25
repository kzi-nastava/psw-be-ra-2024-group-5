using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Encounters.API.Dtos;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/encounter/participant")]
    public class ParticipantController : BaseApiController
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpGet]
        public ActionResult<ParticipantDto> GetByUserId(long userId)
        {
            var result = _participantService.GetByUserId(userId);
            return CreateResponse(result);
        }
    }
}
