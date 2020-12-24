using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;



namespace dotNet5781_03B_9106_7647
{
    //public interface INotifyPropertyChanged;

    public class BUS
    {

        System.Windows.Threading.DispatcherTimer DispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer DispatcherTimer1 = new System.Windows.Threading.DispatcherTimer();
        System.Windows.Threading.DispatcherTimer DispatcherTimer2 = new System.Windows.Threading.DispatcherTimer();
        public enum status { ReadyToTravel, OnTravel, OnRefueling, OnTreatment }
        private
            string numberbus;
        public string Num
        {
            get => numberbus;   // line number
            set => numberbus = value;
        }

        DateTime Datebegin;

        public DateTime Dates { get => Datebegin; set => Datebegin = value; }  // date of first activity

        DateTime Datetreatment;
        public DateTime Datetr { get => Datetreatment; set => Datetreatment = value; } //date since the last treatment
        int km;  // km depuis le tipoul
        public int Km { get => km; set => km = value; }      // number of kilometers since the last treatment
        int kmbegining;
        public int KmBegin { get => kmbegining; set => kmbegining = value; } // number of kilometers since the first activity
        int gasoline = 1200;
        public int Gasoline
        {             //gasoline rate
            get => gasoline;
            set => gasoline = value;
        }



        status Status;

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged( string name)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        //}

        public status statusp
        {
            get => Status;
            set => Status = value;
        }
        public BUS()
        {
            ;
        }
        public
           BUS(string busnumber, DateTime date, DateTime date2, int kmb, int kmlt, int stat, int gas)   // BUS constructor
        {
            numberbus = busnumber;
            Datebegin = date;
            Datetreatment = date2;
            Status = (status)stat;
            km = kmlt;
            kmbegining = kmb;
            gasoline = gas;
        }


        public override string ToString()
        {
            return numberbus;
        }


        public bool VerifBusCanTravel(int k)
        {
            if (statusp == 0)
            {
                if (k > Gasoline)
                    return false;
                if ((km + k <= 20000) && (gasoline >= k) && ((DateTime.Now - Datetr).Days < 365))
                {
                    KmBegin += k;
                    Km += k;
                    statusp = (status)1;
                    Random r = new Random();
                    float v = r.Next(20, 50);
                    float time = k / v;
                    time = time * 6;
                    DispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                    DispatcherTimer.Interval = new TimeSpan(0, 0, 0, (int)time);
                    DispatcherTimer.Start();
                    return true;

                }

            }
            return false;
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer.Stop();
            statusp = (status)0;
            
          //  OnPropertyChanged(status);


        }
        public void DoTreatment()
        {
            Datetr = DateTime.Now;
            statusp = (status)3;
            DispatcherTimer1.Tick += new EventHandler(dispatcherTimer_Tick);
            DispatcherTimer1.Interval = new TimeSpan(0, 0, 12);//2 hours for treatment
            DispatcherTimer1.Start();

        }

        public void Refueling()
        {
            Gasoline += (1200 - Gasoline);
            statusp = (status)2;
            DispatcherTimer2.Tick += new EventHandler(dispatcherTimer_Tick);
            DispatcherTimer2.Interval = new TimeSpan(0, 0, 3);// 30 min for refueling
            DispatcherTimer2.Start();
        }

    }

}
