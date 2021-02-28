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



        public Window18()
        {
            InitializeComponent();
            DataContext = new CurrentTimeViewModel();
          
        }

        public void init()
        {
            CbHours.ItemsSource = bl.Listhours();
            CbMin.ItemsSource = bl.ListMinOrSec();
            CbSec.ItemsSource = bl.ListMinOrSec();
           tbStation.DataContext = CurrentStation;
           tbStation.Text = CurrentStation.Name;
         
        }
       
        private void timer1_Elapsed(/*object sender, EventArgs e*/)
        {
            BO.Simulator S = bl.LinesFromStation(CurrentStation);
            TimeSpan t = new TimeSpan();
            t = new TimeSpan(Int32.Parse((string)CbHours.SelectedItem), Int32.Parse((string)CbMin.SelectedItem),Int32.Parse((string) CbSec.SelectedItem));
            List<TimeSpan> listtimespan = new List<TimeSpan>();
               listtimespan= bl.StartSimulator(Int32.Parse(TbSpeed.Text), t, S);
            ListBoxLineNumber.ItemsSource = S.ListLines;
            ListBoxTime.ItemsSource = listtimespan;
            ListBoxTime.Items.Refresh();
        }

        public class CurrentTimeViewModel : INotifyPropertyChanged
        {
            private string _currentTime;

            public CurrentTimeViewModel()
            {
                UpdateTime();
            }

            private async void UpdateTime()
            {
                CurrentTime = DateTime.Now.ToString("G");
                await Task.Delay(1000);
                UpdateTime();
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public string CurrentTime
            {
                get { return _currentTime; }
                set { _currentTime = value; OnPropertyChanged(); }
            }
        }

            private void Button_ClickStartStop(object sender, RoutedEventArgs e)
        {
            if (StartStop.Content == "Start")
            {
                StartStop.Content = "Stop";
                ((ComboBox)CbHours).IsEnabled = false;
                ((ComboBox)CbMin).IsEnabled = false;
                ((ComboBox)CbSec).IsEnabled = false;
                ((TextBox)TbSpeed).IsEnabled = false;
                IEnumerable<BO.Line> listLine = bl.LinesFromStation(CurrentStation).ListLines;
                /// InitTimer();
                timer1_Elapsed();

            }
            else
            {
                StartStop.Content = "Start";

                ((ComboBox)CbHours).IsEnabled = true;
                ((ComboBox)CbMin).IsEnabled = true;
                ((ComboBox)CbSec).IsEnabled = true;
                ((TextBox)TbSpeed).IsEnabled = true;

            }

        }

       
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

   

    }
}
