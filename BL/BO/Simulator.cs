using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class Simulator
    {
       public IEnumerable<BO.Line> ListLines;
       public List<TimeSpan> ListArrivalTimes = new List<TimeSpan>();
    }
}
