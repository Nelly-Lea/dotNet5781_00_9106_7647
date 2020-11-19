using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using System.Device.Location;



namespace dotNet5781_02_9106_7647

{



    interface IComparable

    {

    }

    public class busline : IComparable

    {



        public enum Area

        {

            General, north, south, west, east, jerusalem, center, telaviv, hadera, tiberiade, ranana

        }

        List<busstationline> Stations = new List<busstationline>();



        public List<busstationline> stations

        {

            get => Stations;

            set => Stations = value;

        }

        int BusLine = 0; // number bus line

        public int buslinenumber
        {

            get => BusLine;

            set => BusLine = value;
        }

        private busstation FirstStation;

        public busstation firststation

        {

            get => FirstStation;

            set => FirstStation = value;

        }

        private busstation LastStation;

        public busstation laststation

        {

            get => LastStation;

            set => LastStation = value;

        }

        Area area;

        public Area areap

        {

            get => area;

            set => area = value;

        }

        //constructors

        public busline(int num, busstation f, busstation l, Random r)

        {

            BusLine = num;

            FirstStation = f;

            LastStation = l;

            area = (Area)r.Next(0, 10);



        }

        public busline(busstation f, busstation l)

        {

            FirstStation = f;

            LastStation = l;

        }

        public busline(int num, Random r)

        {

            BusLine = num;

            var rand = new Random();

            area = (Area)r.Next(0, 10);

        }

        public override string ToString() => "BusLine " + BusLine + " Area: " + area + " first station: " + FirstStation.ToString() + " last station: " + LastStation.ToString();

        public void addstation(int key, Random r, List<busstation> BS)

        {



            busstationline bs = new busstationline();

            foreach (var item in BS)

            {

                if (item.busstationkey == key)

                {

                    bs.busstationkey = item.busstationkey;

                    bs.latitude = item.latitude;

                    bs.longitude = item.longitude;

                    break;

                }



            }



            if (stations.Count() != 0)

            {

                GeoCoordinate distfrom = new GeoCoordinate();

                GeoCoordinate distto = new GeoCoordinate();

                distfrom.Latitude = stations[stations.Count() - 1].latitude;

                distfrom.Longitude = stations[stations.Count() - 1].longitude;

                distto.Latitude = bs.latitude;

                distto.Longitude = bs.longitude;

                double distance = distfrom.GetDistanceTo(distto);

                double time = distance / 1166.67; // v=d/t 70km/h=1166.67 m/min

                bs.lenghtlaststation = distance;

                bs.timelaststation = time;

            }

            Stations.Add(bs);

            LastStation = bs;

            if (stations.Count() > 1)

                FirstStation = stations[0];

        }



        public void addstation(ref busstationline bsl)

        {



            if (stations.Count() != 0)

            {

                var distfrom = new GeoCoordinate();

                var distto = new GeoCoordinate();

                distfrom.Latitude = stations[stations.Count() - 1].latitude;

                distfrom.Longitude = stations[stations.Count() - 1].longitude;

                distto.Latitude = bsl.latitude;

                distto.Longitude = bsl.longitude;

                double distance = distfrom.GetDistanceTo(distto);

                double time = distance / 1166.67; // v=d/t 70km/h=1166.67 m/min we calculate the time according to this formula(we choose that the vitesse is 70 km/h)

                bsl.lenghtlaststation = distance;

                bsl.timelaststation = time;

            }

            Stations.Add(bsl);

            LastStation = bsl;

        }



        public void substation(busstationline b)

        {

            if (b.busstationkey == firststation.busstationkey)

            {

                firststation = stations[1];

            }



            if (b.busstationkey == laststation.busstationkey)

            {

                laststation = stations[stations.Count - 2];

            }

            Stations.Remove(b);



        }



        public busstation searchstation2(int buskey)

        {

            foreach (var item in Stations)

            {

                if (item.busstationkey == buskey)

                {

                    return item;

                }

            }

            return null;

        }

        public double distance(busstation a, busstation b)

        {

            double distance = 0;

            int i;

            for (i = Stations.Count; i >= 0; i--)

            {

                if (Stations[i].busstationkey == b.busstationkey)

                {

                    distance = Stations[i].lenghtlaststation;

                    break;

                }



            }

            for (; i >= 0; i--)

            {

                distance += Stations[i].lenghtlaststation;

                if (Stations[i].busstationkey == a.busstationkey)

                    break;



            }

            return distance;

        }

        public double timebetween2stations(busstation a, busstation b)

        {

            if (stations.Contains(a) && (stations.Contains(b)))
            {

                double time = 0;

                int i;

                for (i = (Stations.Count - 1); i >= 0; i--)

                {

                    if (Stations[i].busstationkey == b.busstationkey)

                    {

                        time = Stations[i].timelaststation;

                        break;

                    }



                }

                i--;

                for (; i >= 0; i--)

                {



                    if (Stations[i].busstationkey == a.busstationkey)

                        break;

                    time += Stations[i].timelaststation;



                }

                return time;

            }

            return 0;



        }

        public busline sub_route(busstation a, busstation b, Random r)

        {

            busline Bus = new busline(BusLine, a, b, r);

            int i;

            for (i = 0; i < Stations.Count; i++)

            {

                if (Stations[i].busstationkey == a.busstationkey)

                    break;

            }

            for (; i < Stations.Count; i++)

            {

                Bus.Stations.Add(Stations[i]);

                if (Stations[i].busstationkey == b.busstationkey)

                    break;

            }

            return Bus;

        }

        public int CompareTo(object obj, busstation c, busstation d)

        {

            busline b = (busline)obj;



            return timebetween2stations(c, d).CompareTo(b.timebetween2stations(c, d));

        }

        public int compareTo2(object obj)

        {

            busline b = (busline)obj;

            return timebetween2stations(firststation, laststation).CompareTo(b.timebetween2stations(b.firststation, b.laststation));

        }

        public void printbusline()  // print the number line bus

        {

            Console.WriteLine("The bus number is: {0}", BusLine);

        }

    }

}

