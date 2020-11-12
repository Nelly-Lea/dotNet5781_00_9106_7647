using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_9106_7647
{
   public class busstation
    {
        private int BusStationKey;
        public int busstationkey
        {
            get { return BusStationKey; }
            set { busstationkey = BusStationKey; }
        }
        private double Latitude;
        private double Longitude;

        public busstation(int b, double la, double lo)
        {
            BusStationKey = b;
            Latitude = la;
            Longitude = lo;
        }


        public override string ToString()
        {

            return "Bus Station Code: " + BusStationKey + ", " + Latitude + "°N " + Longitude + "°E"; ;
        }
    }
}
