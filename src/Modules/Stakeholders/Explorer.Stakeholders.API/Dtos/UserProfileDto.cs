using Explorer.Stakeholders.API.Dtos.Messages;

namespace Explorer.Stakeholders.API.Dtos;

public class UserProfileDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? ProfileImage { get; set; }
    public string? Biography { get; set; }
    public string? Motto { get; set; }
    public List<MessageDto> Messages { get; set; }

    public UserProfileDto() { }

    public UserProfileDto(long id, long userId, string name, string surname,
        string? profileImage, string? biography, string? motto, List<MessageDto> messages)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Surname = surname;
        ProfileImage = profileImage;
        Biography = biography;
        Motto = motto;
        Messages = messages;
    }
}
