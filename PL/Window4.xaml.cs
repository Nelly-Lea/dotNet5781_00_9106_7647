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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window4.xaml
    /// </summary>
    
    public partial class Window4 : Window
    {
       public  BO.Line Line;
        static IBL bl2 = BLFactory.GetBL("1");
        public IEnumerable<BO.LineStation> listOfLines;


        public List<BO.LineStation> ListLineStations = new List<BO.LineStation>();
        public BO.LineStation CurrentLineStation=new BO.LineStation();


        public Window4()
        {
            //listOfLines = bl2.GetLineStation(Line.Id);
            //var listLineStationsObs = new ObservableCollection<BO.LineStation>(listOfLines);
            //ListLinesStation.ItemsSource = listLineStationsObs; 

            InitializeComponent();
            this.DataContext = this;
            

        }
        public void InitList()
        {
            var listLineStationsObs = new ObservableCollection<BO.LineStation>(ListLineStations);
            ListLinesStation.ItemsSource = listLineStationsObs;
            ListLinesStation.Items.Refresh();

        }
        private void RemoveLineStation_button(object sender, RoutedEventArgs e)
        {


            if (sender != null && sender is Button btn)
                CurrentLineStation = (BO.LineStation)btn.DataContext;
            bl2.RemoveLineStation(Line,CurrentLineStation.Station);
            ListLinesStation.Items.Refresh();
            Window4 win4 = new Window4();
            Application.Current.MainWindow = win4;
            win4.Show();
            //this.Close();


        }

        private void UpdateLineStation_button(object sender, RoutedEventArgs e)
        {
        //    if (sender != null && sender is Button btn)
        //        CurrentLineStation = (BO.LineStation)btn.DataContext;

        //    Window5 win5 = new Window5();
            
        //    List<BO.LineStation> L = new List<BO.LineStation>();
        //    L = bl1.GetLineStation(CurrentLine1.Id).ToList();
        //    win5.ListLineStations = bl1.GetLineStation(CurrentLine1.Id).ToList();
        //    win5.Line = CurrentLine1;
        //    win5.InitList();
        //    win5.ShowDialog();
        }





    }
}
