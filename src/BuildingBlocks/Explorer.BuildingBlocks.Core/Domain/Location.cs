﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.BuildingBlocks.Core.Domain;

public class Location : ValueObject {
    public double Latitude { get; init; }
    public double Longitude { get; init; }

    private Location() { }

    [JsonConstructor]
    public Location(double latitude, double longitude) {
        Latitude = latitude;
        Longitude = longitude;
    }

    protected override IEnumerable<object> GetEqualityComponents() {
        yield return Latitude;
        yield return Longitude;
    }
}
