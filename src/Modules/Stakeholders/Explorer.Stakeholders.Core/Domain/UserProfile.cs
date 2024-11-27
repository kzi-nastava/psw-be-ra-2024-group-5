using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Messages;

namespace Explorer.Stakeholders.Core.Domain;

public class UserProfile : Entity
{
    public long UserId { get; private set; } 
    public byte[]? ProfileImage { get; private set; } 
    public string? Biography { get; private set; } 
    public string? Motto { get; private set; } 
    public List<ProfileMessage> ProfileMessages { get; init; } = new List<ProfileMessage>();

    public UserProfile(long userId, byte[]? profileImage, string? biography, string? motto)
    {
        UserId = userId;
        ProfileImage = profileImage;
        Biography = biography;
        Motto = motto;
        Validate();
    }

    public UserProfile(long userId)
    {
        UserId = userId;
        Biography = Motto = string.Empty;
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
    public void setBiography(string biography) { this.Biography = biography; }
    public void setMotto(string motto) { this.Motto = motto; }
    public void setProfileImage(byte[] profileImage) { this.ProfileImage = profileImage; }
}
