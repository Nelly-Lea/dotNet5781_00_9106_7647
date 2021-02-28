using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public  class TravelBetween2Stations // we use this class in the window 14. we return all lines and their last stations
                                        // passing through 2 stations, the list of stations in the travel between 
                                       // these 2 stations and the number of stations in the travel. 
    {
        public List<BO.Line> ListLines = new List<BO.Line>();
        public List<List<BO.Station>> ListStations = new List<List<BO.Station>>();
        public List<int> ListNumberStationBetween2Stations = new List<int>();
        public List<string> ListLastStation = new List<string>();
    }
}
