using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_9106_7647
{
    public class busstationline :busstation
    {

        private int LenghtLastStation;
        public int lenghtlaststation
        {
            get { return LenghtLastStation; }
            set { LenghtLastStation = lenghtlaststation; }
        }
        private int TimeLastStation;
        public int timelaststation
        {
            get { return TimeLastStation; }
            set { TimeLastStation = timelaststation; }
        }
        public busstationline(int b, double la, double lo, int le, int t) : base(b, la, lo)
        {
            LenghtLastStation = le;
            TimeLastStation = t;
        }
    }
}
