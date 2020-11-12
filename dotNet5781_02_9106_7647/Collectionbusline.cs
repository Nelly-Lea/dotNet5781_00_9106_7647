using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_9106_7647
{
    public interface IEnumerator<T> { }
    //public class collectionbuslineEnumerator<T>:IEnumerator<T>
    //{
    //    private int indice;
    //    private Collectionbusline<T> collbusline;
    //    public collectionbuslineEnumerator(Collectionbusline<T> collection)
    //    {
    //        indice = -1;
    //        collbusline = collection;
    //    }
    //    public bool MoveNext
    //}
    public interface IEnumerable { }
    public class Collectionbusline: IEnumerable
    {
        List<busline> collectionbusline ;
        public IEnumerator GetEnumerator()
        {
            return collectionbusline.GetEnumerator();
        }
        public void addbusline(busline b)
        {
            collectionbusline.Add(b);

        }

        public void removebusline(busline b)
        {
            collectionbusline.Remove(b);
        }
    }
}
