using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class ListTimer//this class return an object with list of line number, list of timers and list of last stations
    {
        public List<BO.Line> ListLines = new List<BO.Line>();
        public List<TimeSpan> listTimer = new List<TimeSpan>();
        public List<string> listLastStation = new List<string>();

    }
}
