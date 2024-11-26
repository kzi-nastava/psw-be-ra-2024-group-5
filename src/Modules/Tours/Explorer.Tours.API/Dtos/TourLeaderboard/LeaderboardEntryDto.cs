namespace Explorer.Tours.API.Dtos.TourLeaderboard;

public class LeaderboardEntryDto
{
    public int Position { get; set; }
    public long UserId { get; set; }
    public string UserName { get; set; }
    public string? ProfileImage { get; set; }
    public TimeSpan Time { get; set; }
}