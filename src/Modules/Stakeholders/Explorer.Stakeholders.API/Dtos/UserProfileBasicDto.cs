namespace Explorer.Stakeholders.API.Dtos;

public class UserProfileBasicDto
{
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? ProfileImage { get; set; }

    public UserProfileBasicDto(long userId, string name, string surname, string? profileImage)
    {
        UserId = userId;
        Name = name;
        Surname = surname;
        ProfileImage = profileImage;
    }
}