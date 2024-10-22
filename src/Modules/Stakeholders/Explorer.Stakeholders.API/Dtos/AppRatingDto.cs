
namespace Explorer.Stakeholders.API.Dtos
{
    public class AppRatingDto
    {
        public int Grade { get; set; }
        public long Id { get; set; }

        public DateTime TimeStamp { get; set; }
        public long UserId { get; set; }
        public string Comment { get; set; }
    }
}
