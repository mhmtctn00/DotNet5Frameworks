using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers
{
    public class CoordinateHelper
    {
        public static LatLongCoordinate Move(LatLongCoordinate coordinate, int xMeters, int yMeters)
        {
            var kmDeg = 0.0090;
            var piDivide180 = Math.PI / 180;


            double xCoef = xMeters * kmDeg;
            double yCoef = yMeters * kmDeg;

            double newLat = coordinate.Latitude + yCoef;

            double newLong = coordinate.Longitude + xCoef / Math.Cos(coordinate.Latitude * piDivide180);

            coordinate = new LatLongCoordinate(newLat, newLong);

            return coordinate;
        }
        public static List<LatLongCoordinate> GetCoordinatesThatAreInCircle(List<LatLongCoordinate> coordinates, LatLongCoordinate centerCoordinate, int radius)
        {
            var respList = new List<LatLongCoordinate>();
            var centerX = centerCoordinate.Latitude * 110.574;
            var centerY = Math.Cos(centerCoordinate.Latitude) * 111.320;
            foreach (var coor in coordinates)
            {
                var coorX = coor.Latitude * 110.574;
                var coorY = Math.Cos(coor.Latitude) * 111.320;

                if (radius >= Math.Sqrt(Math.Pow(centerX - coorX, 2) + Math.Pow(centerY - coorY, 2)))
                {
                    respList.Add(coor);
                }
            }

            return respList;
        }
        public static void Sort(LatLongCoordinate centerCoor, ref List<LatLongCoordinate> coorList)
        {
            for (int i = 0; i < coorList.Count; i++)
            {
                for (int j = i + 1; j < coorList.Count - 1; j++)
                {
                    if (GetDistanceFromLatLonInKm(centerCoor, coorList[i]) > GetDistanceFromLatLonInKm(centerCoor, coorList[j]))
                    {
                        var temp = coorList[j];
                        coorList[j] = coorList[i];
                        coorList[i] = temp;
                    }
                }
            }
        }
        public static double GetDistanceFromLatLonInKm(LatLongCoordinate coor1, LatLongCoordinate coor2)
        {
            var R = 6371; // Radius of the earth in km
            var dLat = Deg2Rad(coor2.Latitude - coor1.Latitude);  // deg2rad below
            var dLon = Deg2Rad(coor2.Longitude - coor1.Longitude);
            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(Deg2Rad(coor1.Latitude)) * Math.Cos(Deg2Rad(coor2.Latitude)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d;
        }

        public static double Deg2Rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
    public class LatLongCoordinate
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public LatLongCoordinate(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }
        public override string ToString()
        {
            return $"{this.Latitude},{this.Longitude}";
        }
    }
}
