using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace DO
{
    [Serializable]
    public class AdjacentStations
    {
        public int id { get; set; }
        public int Station1 { get; set; }
        public int Station2 { get; set; }
        public double Distance { get; set; }
        [XmlIgnore]

        public TimeSpan Time { get; set; }
        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "Time")]
        public string TimeStartAtEventString
        {
            get
            {
                return XmlConvert.ToString(Time);
            }
            set
            {
                Time = string.IsNullOrEmpty(value) ?
                    TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }

    }
}
