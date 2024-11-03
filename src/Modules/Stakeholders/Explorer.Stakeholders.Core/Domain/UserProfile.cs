using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain;

public class UserProfile : Entity
{
    public long UserId { get; private set; } 
    public string? ProfilePictureUrl { get; private set; } 
    public string? Biography { get; private set; } 
    public string? Motto { get; private set; } 
    public List<Message> Messages { get; private set; } = new List<Message>();

    public UserProfile(long userId, string? profilePictureUrl, string? biography, string? motto)
    {
        UserId = userId;
        ProfilePictureUrl = profilePictureUrl;
        Biography = biography;
        Motto = motto;
        Validate();
    }
    public UserProfile() { }

    private void Validate()
    {
        if (UserId == 0) throw new ArgumentException("Invalid UserId");
        Messages.ForEach(m => m.Validate());
    }

    public void AddMessage(Message message)
    {
        Messages.Add(message);
        Validate();
    }

    public void MarkAllMessagesAsRead()
    {
        Messages.ForEach(m => m.MarkAsRead());
        Validate();
    }

    public void setUserId(long userId) { this.UserId = userId; }
    public void setProfilePictureUrl(string profilePictureUrl) { this.ProfilePictureUrl = profilePictureUrl; }
    public void setBiography(string biography) { this.Biography = biography; }
    public void setMotto(string motto) { this.Motto = motto; }
}
