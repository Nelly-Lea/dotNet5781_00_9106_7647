using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ShowStationsLine // we use this class in window 9.  We return all stations in a line, all distances and times with the next station
    {
        public List<Station> ListStat=new List<Station>();
        public List<double> ListDistances=new List<double>();
        public List<TimeSpan> ListTimes=new List<TimeSpan>();
    }
}
