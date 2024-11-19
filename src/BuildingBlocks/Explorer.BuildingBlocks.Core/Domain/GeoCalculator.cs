using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.BuildingBlocks.Core.Domain;
public class GeoCalculator {
    public static bool IsClose(Location position1, Location position2, double thresholdInMeters) {
        const double EarthRadius = 6371000;

        double lat1Rad = DegreesToRadians(position1.Latitude);
        double lng1Rad = DegreesToRadians(position1.Longitude);
        double lat2Rad = DegreesToRadians(position2.Latitude);
        double lng2Rad = DegreesToRadians(position2.Longitude);

        double dLat = lat2Rad - lat1Rad;
        double dLng = lng2Rad - lng1Rad;

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                    Math.Sin(dLng / 2) * Math.Sin(dLng / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double distance = EarthRadius * c;

        return distance <= thresholdInMeters;
    }

    private static double DegreesToRadians(double degrees) {
        return degrees * Math.PI / 180;
    }
}
