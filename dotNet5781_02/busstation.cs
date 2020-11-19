using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



namespace dotNet5781_02_9106_7647

{



    public class busstation

    {

        protected int BusStationKey;

        public int busstationkey

        {

            get { return BusStationKey; }

            set { BusStationKey = value; }

        }

        protected double Latitude;

        protected double Longitude;



        public double latitude

        {

            get => Latitude;

            set => Latitude = value;

        }

        public double longitude

        {

            get => Longitude;

            set => Longitude = value;

        }



        public busstation(int b)

        {

            BusStationKey = b;

        }

        public busstation(int b, Random r)

        {

            BusStationKey = b;



            Latitude = r.NextDouble() * (33.3 - 31) + 31;

            Longitude = r.NextDouble() * (35.5 - 34.3) + 34.4;

        }

        public busstation()

        {

            BusStationKey = 0;

            Latitude = 0;

            Longitude = 0;



        }



        public override string ToString()

        {



            return "Bus Station Code: " + BusStationKey + ", " + Latitude + "°N " + Longitude + "°E"; ;

        }

    }

}


