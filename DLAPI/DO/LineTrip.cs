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
    public class LineTrip
    {
        public int Id { get; set; }
        public int LineId { get; set; }
        [XmlIgnore]
        public TimeSpan StartAt { get; set; }
        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "StartAt")]
        public string TimeStartAtEventString
        {
            get
            {
                return XmlConvert.ToString(StartAt);
            }
            set
            {
                StartAt = string.IsNullOrEmpty(value) ?
                    TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }
        [XmlIgnore]
       
        public TimeSpan Frequency { get; set; }
        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "Frequency")]
        public string TimeFrequencytEventString
        {
            get
            {
                return XmlConvert.ToString(Frequency);
            }
            set
            {
                Frequency = string.IsNullOrEmpty(value) ?
                    TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }
        [XmlIgnore]
        public TimeSpan FinishAt { get; set; }
        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "FinishAt")]
        public string TimeFinishAtEventString
        {
            get
            {
                return XmlConvert.ToString(FinishAt);
            }
            set
            {
                FinishAt = string.IsNullOrEmpty(value) ?
                 TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }



    }
}
