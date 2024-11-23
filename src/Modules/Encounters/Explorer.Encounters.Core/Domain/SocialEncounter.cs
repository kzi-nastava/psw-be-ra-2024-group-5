using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain;
public class SocialEncounter : Encounter
{
    public float Radius { get; set; }
    public int PeopleCount { get; set; }
    public ICollection<long> UserIds { get; set; } = new List<long>();

    public SocialEncounter() { }

    public SocialEncounter(float radius, int peopleCount, List<long> userIds)
    {
        Radius = radius;
        PeopleCount = peopleCount;
        UserIds = userIds;
    }

    public bool CanBeCompletedByUser(long userId) {
        return UserIds.Contains(userId) && UserIds.Count == PeopleCount;
    }

    public void AddUser(long userId) {
        UserIds.Add(userId);
    }
}
