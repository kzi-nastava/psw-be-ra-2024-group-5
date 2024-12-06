using Explorer.BuildingBlocks.Core.Domain;


namespace Explorer.Stakeholders.Core.Domain
{
    public class AppRating : Entity
    {
        public int Grade { get; init; }
        public DateTime TimeStamp { get; init; }
        public long UserId { get; init; }
        public string Comment { get; init; }

        public AppRating(int grade, DateTime timeStamp, long userId, string comment)
        {
            if (grade < 1 || grade > 5)
            {
                throw new ArgumentException("Invalid grade");
            }
            Grade = grade;
            TimeStamp = timeStamp != default ? timeStamp : DateTime.UtcNow;
            UserId = userId;
            Comment = comment;
        }
    }
}
