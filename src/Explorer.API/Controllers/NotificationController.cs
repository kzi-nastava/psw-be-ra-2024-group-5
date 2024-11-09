using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers;

[Route("api/notifications")]
public class NotificationController: BaseApiController
{
    private readonly INotificationService _notificationService;
    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet("{userId}/paged")]
    public ActionResult<PagedResult<NotificationDto>> GetPagedNotifications(long userId, int page = 1, int pageSize = 10)
    {
        var result = _notificationService.GetPagedNotifications(userId, page, pageSize);

        if (result.IsFailed)
        {
            return NotFound();
        }

        return Ok(result.Value);
    }

    [HttpPost("{notificationId}/read")]
    public ActionResult MarkNotificationAsRead(long notificationId)
    {
        var result = _notificationService.MarkNotificationAsRead(notificationId);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpPost("{userId}/mark-all-read")]
    public ActionResult MarkAllNotificationsAsRead(long userId)
    {
        var result = _notificationService.MarkAllNotificationsAsRead(userId);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpPost("{userId}/send")]
    public ActionResult<NotificationDto> SendNotification(long userId, [FromBody] NotificationDto notificationDto)
    {
        var result = _notificationService.SendNotification(userId, notificationDto);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors);  
        }

        return Ok(result.Value);   
    }

}
