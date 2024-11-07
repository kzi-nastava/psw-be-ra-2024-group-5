using Explorer.Stakeholders.API.Dtos.Messages;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Identity
{
    [Authorize(Policy = "touristPolicy")]
    [Authorize(Policy = "authorPolicy")]
    [Route("api/messages")]
    public class MessagesController : BaseApiController 
    {
        private readonly IProfileMessagesService _profileMessagesService;

        public MessagesController(IProfileMessagesService profileMessagesService)
        {
            _profileMessagesService = profileMessagesService;
        }

        [HttpPost("profile/message/send")]
        public ActionResult<MessageDto> SendMessage([FromBody] SendMessageDto request)
        { 
            var result = _profileMessagesService.SendMessage(request.SenderId, request.RecipientId,
                request.Content, request.Attachment);
            return CreateResponse(result);
        }

        [HttpPost("profile/message/mark-viewed")]
        public ActionResult ViewMessage([FromBody] ViewMessageDto request)
        {
            var result = _profileMessagesService.ViewMessage(request.ProfileId, request.MessageId);
            return CreateResponse(result);
        }
    }
}
