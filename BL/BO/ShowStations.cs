using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ShowStations
    {
        // <summary>
        //   we use this class by double click in the window 3 tab station and for the list of adjacent stations in tab adjacent stations
        // we return an object with 4 lists: a list of stations in specific order. According to the index of a station in this list we can knwow
        // the index of the list of line number in lineNumbers and  last stations in lastStationNames corresponding to the correct station.
        // adjtations : we return all adjacent staions
        // </summary>
        public List<Station> stations=new List<Station>();
        public List<AdjacentStations> adjStations=new List<AdjacentStations>();
        public List<List<BO.Line>> linesNumbers = new List<List<BO.Line>>();
        public List<List<string>> lastStationNames = new List<List<string>>();
    }
}
