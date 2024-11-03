using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourTouristDto
    {
        public TourDto Tour { get; set; }

        //polja za prikaz turiste settuju se bez konstruktora
        public bool CanBeActivated { get; set; } = false;
        public bool CanBeBought { get; set; } = false;
        public bool CanBeReviewed { get; set; } = false;
        public TourTouristDto() { }

        public TourTouristDto(TourDto tour) {
            Tour = tour;
        }
    }
}
