using Explorer.BuildingBlocks.Core.Domain;

namespace Explorer.Tours.Core.Domain;

public enum TourLevel { Beginner, Intermediate, Advanced}
public enum TourStatus { Draft, Active, Finished, Canceled = -1}

public class Tour : Entity {
    public string Name { get; private set; }
    public string Description { get; private set; }
    public TourLevel Level { get; private set; }
    public TourStatus Status { get; private set; }
    public double Price { get; private set; }
    public ICollection<Equipment> Equipments { get; set; } = new List<Equipment>();
    
    public Tour(string name, string? description,  TourLevel level) {
        if(string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("Invalid Name.");
        
        Name = name;
        Description = description;
        Level = level;
        Status = TourStatus.Draft;
        Price = 0.0;
    }

}