using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Windows.Threading;
using System.Threading;

//using System.Runtime.CompilerServices;
using BLAPI;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window18.xaml
    /// </summary>
    public partial class Window18 : Window
    {
        static IBL bl = BLFactory.GetBL("1");
        public BO.Station CurrentStation;
        public TimeSpan timeNow = new TimeSpan();
        public string button;
        public List<List<TimeSpan>> listtimespan = new List<List<TimeSpan>>();
        public BO.Simulator S = new BO.Simulator();
        public BO.ListTimer ListTimer = new BO.ListTimer();
       public DispatcherTimer timer = new DispatcherTimer();
       public DispatcherTimer LiveTime = new DispatcherTimer();
        public Window18()
        {
            InitializeComponent();

            //DataContext = new CurrentTimeViewModel();
            //CurrentTimeViewModel.button1 = StartStop.Content.ToString();

           

        }
        void timer_Tick1(object sender, EventArgs e)
        {
            LabelTimer.Content = DateTime.Now.ToString("HH:mm:ss");
        }
        public void init()
        {
            CbHours.ItemsSource = bl.Listhours();
            CbMin.ItemsSource = bl.ListMinOrSec();
            CbSec.ItemsSource = bl.ListMinOrSec();
            tbStation.DataContext = CurrentStation;
            tbStation.Text = CurrentStation.Name;
            
            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += timer_Tick1;
            LiveTime.Start();

        }

        //private void timer1_Elapsed(/*object sender, EventArgs e*/)
        //{
        //    BO.Simulator S = bl.LinesFromStation(CurrentStation);
        //    TimeSpan t = new TimeSpan();
        //    t = new TimeSpan(Int32.Parse((string)CbHours.SelectedItem), Int32.Parse((string)CbMin.SelectedItem), Int32.Parse((string)CbSec.SelectedItem));
        //    List<TimeSpan> listtimespan = new List<TimeSpan>();
        //    listtimespan = bl.StartSimulator(Int32.Parse(TbSpeed.Text), t, S);
        //    ListBoxLineNumber.ItemsSource = S.ListLines;
        //    ListBoxTime.ItemsSource = listtimespan;
        //    ListBoxTime.Items.Refresh();

        //}

        //public class CurrentTimeViewModel : INotifyPropertyChanged
        //{
        //    private string _currentTime;
        //    public static string button1;
        //    //public void f()
        //    public  CurrentTimeViewModel()
        //    {

        //        UpdateTime();
        //    }

        //    private async  void UpdateTime()
        //    {

        //        CurrentTime = DateTime.Now.TimeOfDay.ToString("G");

        //        await Task.Delay(1000);
        //        if (CurrentTimeViewModel.button1 == "Start")
        //        {
        //            UpdateTime();
                    
                    
        //        }


        //    }


        //    public event PropertyChangedEventHandler PropertyChanged;

        //    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //    {
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //    }

        //    public string CurrentTime
        //    {
        //        get { return _currentTime; }
        //        set { _currentTime = value; OnPropertyChanged(); }
        //    }
        //}

        //public void GetLabelTime(TimeSpan t)
        //{
        //    LabelTimer.Content = t;
        //}

        private void Button_ClickStartStop(object sender, RoutedEventArgs e)
        {
            if (StartStop.Content.ToString() == "Start")
            {
                StartStop.Content = "Stop";
               
                  
                  //  CurrentTimeViewModel.button1 = StartStop.Content.ToString();

                    ((ComboBox)CbHours).IsEnabled = false;
                    ((ComboBox)CbMin).IsEnabled = false;
                    ((ComboBox)CbSec).IsEnabled = false;
                    ((TextBox)TbSpeed).IsEnabled = false;
                    IEnumerable<BO.Line> listLine = bl.LinesFromStation(CurrentStation).ListLines;
                /// InitTimer();
                //  timer1_Elapsed();
                LiveTime.Stop();
                    TimeSpan startTime = new TimeSpan(Int32.Parse(CbHours.Text), Int32.Parse(CbMin.Text), Int32.Parse(CbSec.Text));
                    timeNow = startTime;
                    
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += timer_Tick;
                    timer.Start();
                    S = bl.LinesFromStation(CurrentStation);


                    listtimespan = bl.StartSimulator(startTime, S);
                    ListBoxLineNumber.ItemsSource = S.ListLines;
                    ListBoxTime.ItemsSource = listtimespan;
                    ListBoxTime.Items.Refresh();
                

            }
            else
            {
                ListBoxLineNumber.ItemsSource = null;
                ListBoxTime.ItemsSource = null;
                StartStop.Content = "Start";
                //CurrentTimeViewModel.button1 = StartStop.Content.ToString();
                //CurrentTimeViewModel c = new CurrentTimeViewModel();
                ((ComboBox)CbHours).IsEnabled = true;
                ((ComboBox)CbMin).IsEnabled = true;
                ((ComboBox)CbSec).IsEnabled = true;
                ((TextBox)TbSpeed).IsEnabled = true;
                timer.Stop();
                init();
            }


        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }





        void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan t = new TimeSpan(0, 0, Int32.Parse(TbSpeed.Text));

            timeNow += t;
            LabelTimer.Content = timeNow.ToString();
            TimeSpan sec = new TimeSpan(0, 0, 1);
            listtimespan = bl.ListTimer(listtimespan, t);

            //ListBoxTime.ItemsSource = bl.ListTimerMinimun(listtimespan, S.ListLines).listTimer;
            //ListTimer= bl.ListTimerMinimun(listtimespan, S.ListLines);
            // S.ListLines = ListTimer.ListLines;
            // ListBoxTime.ItemsSource = ListTimer.listTimer;

            // ListBoxLineNumber.ItemsSource
            ListTimer = bl.ListTimerMinimun(listtimespan, S.ListLines);
            S.ListLines = ListTimer.ListLines;
            ListBoxLineNumber.ItemsSource = S.ListLines;
            ListBoxTime.ItemsSource = ListTimer.listTimer;
        }
    }

    
}
