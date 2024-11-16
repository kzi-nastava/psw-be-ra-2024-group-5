using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Encounters.Core.Domain;
public class Position : ValueObject
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }

    private Position() { }

    [JsonConstructor]
    public Position(double latitude, double longitude) {
        Latitude = latitude;
        Longitude = longitude;
    }

    protected override IEnumerable<object> GetEqualityComponents() {
        yield return Latitude;
        yield return Longitude;
    }
}
