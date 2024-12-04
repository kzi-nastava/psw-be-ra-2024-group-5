namespace Explorer.Tours.API.Dtos.TourLeaderboard;

public class TourLeaderboardDto
{
    public int TourId { get; set; }
    public List<LeaderboardEntryDto> Entries { get; set; }

    public TourLeaderboardDto(int tourId, List<LeaderboardEntryDto> entries)
    {
        TourId = tourId;
        Entries = entries;
    }
}