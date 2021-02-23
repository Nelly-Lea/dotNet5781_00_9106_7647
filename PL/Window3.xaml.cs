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
using System.Collections;
using BLAPI;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {

        static IBL bl1 = BLFactory.GetBL("1");
        public IEnumerable<BO.Line> listOfLines = bl1.GetAllLines();

        BO.Line CurrentLine = new BO.Line();
        BO.Line CurrentLine1 = new BO.Line();
        public BO.Station CurrentStation;
        BO.AdjacentStations CurrentAdjacentStations;
      
        public Window3()
        {

            InitializeComponent();
           
            var listLinesObs = new ObservableCollection<BO.Line>(listOfLines);
            ListLines.ItemsSource = listLinesObs;
            init();
           
        }
    
        public void init()
        {
            ListViewStations.ItemsSource = bl1.ShowBusStations().stations;
            ListViewAdjStations.ItemsSource = bl1.ShowBusStations().adjStations;
        }
        
        private void Button_Click_RemoveLine(object sender, RoutedEventArgs e)
        {
            if (sender != null && sender is Button btn)
                CurrentLine = (BO.Line)btn.DataContext;
            bl1.DeleteLine(CurrentLine.Id);
            ListLines.Items.Refresh();
            Window3 win3 = new Window3();
            Application.Current.MainWindow = win3;
            win3.Show();
            this.Close();
  
        }
        private void Button_Click_UpdateLine(object sender, RoutedEventArgs e)
        {
            
            
            if (sender != null && sender is Button btn)
                CurrentLine1 = (BO.Line)btn.DataContext;
           
            Window4 win4 = new Window4();
            this.Close();
            // win4.Line = CurrentLine1;
            List<BO.LineStation> L = new List<BO.LineStation>();
            L= bl1.GetLineStation(CurrentLine1.Id).ToList();
            win4.ListLineStations = bl1.GetLineStation(CurrentLine1.Id).ToList();
            win4.Line = CurrentLine1;
            win4.InitList();
            win4.ShowDialog();
            
           

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window8 win8 = new Window8();
            win8.Init();
            win8.Show();
            this.Close();
        }
        private void MouseDoubleClick_ShowLine(object sender, MouseButtonEventArgs e)
        {

            CurrentLine = (BO.Line)ListLines.SelectedItem;
            Window9 win9 = new Window9();
            win9.CurrentLine = CurrentLine;
            win9.Init();
            win9.Show();

            this.Close();


        }

        private void Button_AddStation(object sender, RoutedEventArgs e)
        {
            Window12 win12 = new Window12();
            win12.Init();
            win12.ShowDialog();
            Window3 win3 = new Window3();
            Application.Current.MainWindow = win3;
            
            win3.Show();
            this.Close();

        }
        private void MouseDoubleClick_ShowLineInStation(object sender, MouseButtonEventArgs e)
        {

            CurrentStation = (BO.Station)ListViewStations.SelectedItem;
            Window10 win10 = new Window10();
            win10.CurrentStation = CurrentStation;
            win10.index = ListViewStations.SelectedIndex;
            win10.Init();
            win10.Show();

            this.Close();


        }


        private void Button_Click_RemoveStation(object sender, RoutedEventArgs e)
        {
            if (sender != null && sender is Button btn)
               CurrentStation= (BO.Station)btn.DataContext;
          
            bl1.DeleteStation(CurrentStation.Code);
            ListViewStations.Items.Refresh();
            Window3 win3 = new Window3();
            Application.Current.MainWindow = win3;
            win3.Show();
            this.Close();

        }
        private void Button_Click_UpdateStation(object sender, RoutedEventArgs e)
        {
            if (sender != null && sender is Button btn)
                CurrentStation = (BO.Station)btn.DataContext;
            Window11 win11 = new Window11();
            win11.CurrentStation = CurrentStation;
            win11.init();
            win11.ShowDialog();
            Window3 win3 = new Window3();
            Application.Current.MainWindow = win3;
            win3.Show();
            this.Close();

        }
        private void Button_Click_UpdateAdjStation(object sender, RoutedEventArgs e)
        {
            if (sender != null && sender is Button btn)
                CurrentAdjacentStations = (BO.AdjacentStations)btn.DataContext;
            bl1.UpdateTimeAndDistanceAdjStations(CurrentAdjacentStations);
            Window3 win3 = new Window3();
            Application.Current.MainWindow = win3;
            win3.Show();
            this.Close();

        }

      
    }

   
    

}
