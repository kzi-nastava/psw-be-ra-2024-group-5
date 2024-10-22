

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IAppRatingRepository
    {
        public AppRating GetByUser(long userId);
        
    }
}
