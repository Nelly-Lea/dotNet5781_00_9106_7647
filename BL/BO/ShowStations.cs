using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ShowStations
    {
        public List<Station> stations=new List<Station>();
        public List<AdjacentStations> adjStations=new List<AdjacentStations>();
        public List<List<int>> linesNumbers = new List<List<int>>();
        public List<List<string>> lastStationNames = new List<List<string>>();
    }
}
