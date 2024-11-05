using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IClubRepository
    {
        Club? Get(long clubId);
        PagedResult<Club> GetPaged(int page, int pageSize);
        Club Create(Club club);
        Club Update(Club club);
        void Delete(long clubId);
        bool DeleteMembership(long clubId, long userId);
        ClubMembership? CreateMembership(long clubId, long userId);
        List<ClubMembership> GetAllMemberships();
    }
}
