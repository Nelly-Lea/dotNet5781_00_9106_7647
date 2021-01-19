using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineTrip
    {
        public int Id { get; set; }//identifiant
        public int LineId { get; set; }
        public TimeSpan StartAt { get; set; }
        public TimeSpan Frequency { get; set; }//changer la frequence en fonction du moment de la journee
        public TimeSpan FinishAt { get; set; }



    }
}
