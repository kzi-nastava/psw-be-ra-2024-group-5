using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class FacilityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public FacilityType Type { get; set; }
        public string? Image { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
    public enum FacilityType { Wc, Restaurant, Parking, Other };
}
