using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class Simulator//this class return an object with a list of line numbers and a list of arrival time of the line at the station
    {
       public IEnumerable<BO.Line> ListLines;
       public List<TimeSpan> ListArrivalTimes = new List<TimeSpan>();
    }
}
