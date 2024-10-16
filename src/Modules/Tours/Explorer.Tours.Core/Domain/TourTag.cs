using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public class TourTag : Entity {
    public string Name { get; init; }
    public TourTag(string name) {

        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Invalid tag.");
        Name = name;
    }
}
