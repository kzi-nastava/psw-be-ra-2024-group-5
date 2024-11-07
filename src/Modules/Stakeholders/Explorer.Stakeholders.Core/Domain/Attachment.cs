using Explorer.BuildingBlocks.Core.Domain;
using System.Text.Json.Serialization;

namespace Explorer.Stakeholders.Core.Domain;

public class Attachment : ValueObject
{
    public long ResourceId { get; private set; }
    public ResourceType ResourceType { get; private set; }

    [JsonConstructor]
    public Attachment(long resourceId, ResourceType resourceType)
    {
        ResourceId = resourceId;
        ResourceType = resourceType;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ResourceId;
        yield return ResourceType;
    }

    public long GetResourceId() => ResourceId;
    public ResourceType GetResourceType() => ResourceType;
}

public enum ResourceType
{
    TourResource = 0,
    BlogResource
}