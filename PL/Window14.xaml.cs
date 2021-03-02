using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BLAPI;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window14.xaml
    /// </summary>
    public partial class Window14 : Window
    {
        IBL bl = BLFactory.GetBL("1");
        public BO.Station CurrentStationStart;
        public BO.Station CurrentStationFinish;
        public BO.Line CurrentLine;
        public Window14()
        {
            InitializeComponent();
        }
       
        public void init()
        {
           
                LvListLinesNumber.ItemsSource = bl.ShowTravelsBetween2Stations(CurrentStationStart, CurrentStationFinish).ListLines;
                LbLastStation.ItemsSource = bl.ShowTravelsBetween2Stations(CurrentStationStart, CurrentStationFinish).ListLastStation;
                LbNumberOfStations.ItemsSource = bl.ShowTravelsBetween2Stations(CurrentStationStart, CurrentStationFinish).ListNumberStationBetween2Stations;
           
            
        }

        private void MouseDoubleClick_ShowStations(object sender, MouseButtonEventArgs e)
        {

            CurrentLine = (BO.Line)LvListLinesNumber.SelectedItem;
            LvListStations.ItemsSource = bl.GetAllStationInATravel(LvListLinesNumber.SelectedIndex, CurrentStationStart, CurrentStationFinish);




        }

        private void Button_ClickReturn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
