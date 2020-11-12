using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_9106_7647
{
    interface IComparable
    {
    }
    public class busline:IComparable
    {
        List<busstationline> Stations;
        int BusLine = 0;
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
        busline(busstation f, busstation l)
        {
            FirstStation = f;
            LastStation = l;
        }
        public override string ToString() => "BusLine " + BusLine + "Area" + area + "Stations" + Stations;
        public void addstation(busstationline b)
        {
            Stations.Add(b);

        }
        public void substation(busstationline b)
        {
            Stations.Remove(b);

        }
        public bool searchstation(busstationline b)
        {
            if (Stations.Contains(b) == true)
                return true;
            else
                return false;
        }
        public int distance(busstation a, busstation b)
        {
            int distance = 0;
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
        public int timebetween2stations(busstation a, busstation b)
        {
            int time = 0;
            int i;
            for (i = Stations.Count; i >= 0; i--)
            {
                if (Stations[i].busstationkey == b.busstationkey)
                {
                    time = Stations[i].timelaststation;
                    break;
                }

            }
            for (; i >= 0; i--)
            {
                time += Stations[i].timelaststation;
                if (Stations[i].busstationkey == a.busstationkey)
                    break;

            }
            return time;

        }
        public busline sub_route(busstation a, busstation b)
        {
            busline Bus = new busline(a, b);
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
    }
}
