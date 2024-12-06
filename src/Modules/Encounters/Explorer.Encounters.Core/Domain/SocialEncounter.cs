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

    public bool CheckUserNearby(Location userLocation) {
        return GeoCalculator.IsClose(Location, userLocation, Radius);
    }

    public void AddUser(long userId) {
        if (!UserIds.Contains(userId))
            UserIds.Add(userId);
    }

    public void RemoveUser(long userId) {
        if (UserIds.Contains(userId))
            UserIds.Remove(userId);
    }

    public void Complete() {
        UserIds.Clear();
    }
}
