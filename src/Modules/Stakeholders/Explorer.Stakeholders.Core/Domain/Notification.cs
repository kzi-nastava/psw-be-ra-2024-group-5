using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Messages;

namespace Explorer.Stakeholders.Core.Domain;

public class Notification: Entity
{
    public List<long> UserIds { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public NotificationType Type { get; private set; }
    public long? SenderId { get; private set; } // if the notification is a message from sb
    public long? ProfileMessageId { get; private set; } 
    public long? ClubMessageId { get; private set; }
    public long? ClubId { get; private set; } // if the notification is related to a club activity
    public string? Message { get; private set; }
    public Attachment? Attachment { get; private set; }
    public long? EncounterId { get; private set; }
    public List<NotificationReadStatus> UserReadStatuses { get; set; }
}

public enum NotificationType
{
    ProfileMessage = 0,
    ClubMessage,
    ClubActivity,
    EncounterCreated,  
    EncounterApprovalStatus,
    WalletUpdated
}




