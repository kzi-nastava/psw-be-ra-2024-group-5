using Explorer.Stakeholders.API.Dtos.Messages;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IProfileMessagesService
    {
        Result<MessageDto> SendMessage(long senderId, long recipientId, string content, AttachmentDto? attachment);
        Result ViewMessage(long profileId, long messageId);
    }
}
