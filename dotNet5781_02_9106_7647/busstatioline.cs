using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;


namespace dotNet5781_02_9106_7647
{
    
    public class busstationline :busstation  
    {
        protected double LenghtLastStation;
        public double lenghtlaststation
        {
            get { return LenghtLastStation; }
            set  { LenghtLastStation = value; }
        }
        protected double TimeLastStation;
        public double timelaststation
        {
            get { return TimeLastStation; }
            set { TimeLastStation = value; }
        }

        public busstationline(int b, Random r) : base(b,r)
        {

        }
        public busstationline() : base()
        {

        }
        public busstationline(int b) : base(b)
        {

        }
        public override string ToString()
        {

            return "Bus Station Code: " + BusStationKey + ", " + Latitude + "°N " + Longitude + "°E"; ;
        }
    }
}
