namespace Explorer.Stakeholders.API.Dtos;

public class UserProfileDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? Biography { get; set; }
    public string? Motto { get; set; }
}
