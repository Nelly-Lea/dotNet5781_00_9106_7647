
using System;

using System.Collections.Generic;

using System.Collections;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



namespace dotNet5781_02_9106_7647

{

    public interface IEnumerator<T> { }

    public interface IEnumerable { }

    public class Collectionbusline : IEnumerable

    {

        List<busline> collectionbusline = new List<busline>();

        public List<busline> collec

        {

            get => collectionbusline;

            set => collectionbusline = value;

        }

        public IEnumerator GetEnumerator()

        {

            return collectionbusline.GetEnumerator();

        }

        public void addbusline(busline b)

        {

            foreach (var item in collectionbusline)

            {

                if (b.buslinenumber == item.buslinenumber)

                {

                    b.firststation = item.laststation;

                    b.laststation = item.firststation;

                    b.areap = item.areap;

                }

            }

            collectionbusline.Add(b);



        }



        public void removebusline(busline b)

        {

            collectionbusline.RemoveAll(x => x.buslinenumber == b.buslinenumber);

        }

        public List<busline> BusThatStopInThisStation(int code)

        {

            List<busline> ans = new List<busline>();

            foreach (var item in collectionbusline)

            {

                if (item.stations.Exists(x => x.busstationkey == code))

                {

                    ans.Add(item);

                }

            }



            return ans;

        }



        public void SortTimeBus2()

        {



            collectionbusline.Sort(delegate (busline a, busline b)

            {

                return a.compareTo2(b);



            });





        }

        public List<busline> SortTimeBus(busstation b1, busstation b2)

        {

            List<busline> l = new List<busline>();

            l = collectionbusline;





            l.Sort(delegate (busline a, busline b)

            {



                return a.CompareTo(b, b1, b2);



            });



            return l;



        }

        public List<busline> listwithsubrout(busstation b1, busstation b2, Random r)// this function to not write 2 times the same station

        {

            List<busline> l = new List<busline>();

            foreach (var item in collectionbusline)

            {

                busline b = new busline(b1, b2);

                b = item.sub_route(b1, b2, r);

                l.Add(b);

            }

            return l;

        }

        public busline this[int i]

        {

            get { return collectionbusline[i]; }

            set { collectionbusline[i] = value; }

        }

    }

}


