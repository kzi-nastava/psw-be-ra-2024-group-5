using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Stakeholders.Core.Domain;

public class Follower : Entity
{
    public long UserId { get; private set; }
    public long FollowedUserId { get; private set; }

    public Follower(long userId, long followedUserId)
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
