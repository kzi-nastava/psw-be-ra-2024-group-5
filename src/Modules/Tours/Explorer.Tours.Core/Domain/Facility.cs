using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Tours.API.Dtos;

namespace Explorer.Tours.Core.Domain
{
    public class Facility: Entity
    {
        public string Name { get; init; }
        public string? Description { get; init; }
        public FacilityType Type { get; init; }
        public byte[]? Image { get; init; }
        public double Longitude { get; init; }
        public double Latitude { get; init; }   
        public Facility(string name, string? description, double longitude, double latitude, byte[]? image, FacilityType type = FacilityType.Other)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Invalid Name.");
            Name = name;
            Description = description;
            Longitude = longitude;
            Latitude = latitude;
            Type = type;
            Image = image;
        }
    }
}
