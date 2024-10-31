using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain;

public class Following : Entity
{
    public long UserId { get; private set; }
    public long FollowedUserId { get; private set; }

    public Following(long userId, long followedUserId)
    {
        UserId = userId;
        FollowedUserId = followedUserId;
        Validate();
    }

    private void Validate()
    {
        if (UserId == 0) throw new ArgumentException("Invalid UserId");
        if (FollowedUserId == 0) throw new ArgumentException("Invalid UserId");
    }
}
