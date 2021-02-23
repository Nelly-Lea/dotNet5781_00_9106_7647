using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public  class TravelBetween2Stations
    {
        public List<BO.Line> ListLines = new List<BO.Line>();
        public List<List<BO.Station>> ListStations = new List<List<BO.Station>>();
        public List<int> ListNumberStationBetween2Stations = new List<int>();
        public List<string> ListLastStation = new List<string>();
    }
}
