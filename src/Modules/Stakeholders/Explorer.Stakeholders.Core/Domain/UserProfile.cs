using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain;

public class UserProfile : Entity
{
    public long UserId { get; private set; } 
    public string Name { get; private set; } 
    public string Surname { get; private set; }
    public string? ProfilePictureUrl { get; private set; } 
    public string? Biography { get; private set; } 
    public string? Motto { get; private set; } 

    public UserProfile(long userId, string name, string surname, string? profilePictureUrl, string? biography, string? motto)
    {
        UserId = userId;
        Name = name;
        Surname = surname;
        ProfilePictureUrl = profilePictureUrl;
        Biography = biography;
        Motto = motto;
        Validate();
    }

    private void Validate()
    {
        if (UserId == 0) throw new ArgumentException("Invalid UserId");
        if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
        if (string.IsNullOrWhiteSpace(Surname)) throw new ArgumentException("Invalid Surname");
    }
}
