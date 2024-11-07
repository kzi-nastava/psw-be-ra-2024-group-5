using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Messages;

namespace Explorer.Stakeholders.Core.Domain;

public class UserProfile : Entity
{
    public long UserId { get; private set; } 
    public string? ProfilePictureUrl { get; private set; } 
    public string? Biography { get; private set; } 
    public string? Motto { get; private set; } 
    public List<ProfileMessage> ProfileMessages { get; init; } = new List<ProfileMessage>();

    public UserProfile(long userId, string? profilePictureUrl, string? biography, string? motto)
    {
        UserId = userId;
        ProfilePictureUrl = profilePictureUrl;
        Biography = biography;
        Motto = motto;
        Validate();
    }

    public UserProfile(long userId)
    {
        UserId = userId;
        ProfilePictureUrl = Biography = Motto = string.Empty;
    }

    public UserProfile() { }

    public void Validate()
    {
        if (UserId == 0) throw new ArgumentException("Invalid UserId");
        ProfileMessages.ForEach(m => m.Validate());
    }

    public void AddMessage(ProfileMessage message)
    {
        ProfileMessages.Add(message);
        Validate();
    }

    public bool ViewMessage(long messageId)
    {
        var message = ProfileMessages.FirstOrDefault(m => m.Id == messageId);
        if (message != null)
        {
            message.MarkAsRead();
            return true;
        }
        return false;
    }

    public void MarkAllMessagesAsRead()
    {
        ProfileMessages.ForEach(m => m.MarkAsRead());
        Validate();
    }

    public void setUserId(long userId) { this.UserId = userId; }
    public void setProfilePictureUrl(string profilePictureUrl) { this.ProfilePictureUrl = profilePictureUrl; }
    public void setBiography(string biography) { this.Biography = biography; }
    public void setMotto(string motto) { this.Motto = motto; }
}
