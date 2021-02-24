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
        public BO.Line CurrentLine;

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
            TbLineNumber.DataContext = Line;
            TbLineNumber.Text = Line.Code.ToString();
            TbArea.DataContext = Line;
            TbArea.Text = Line.Area.ToString();
            CurrentLine = Line;
            //Window3 win3 = new Window3();
            //Application.Current.MainWindow = win3;
            //Line = (BO.Line)win3.ListLines.SelectedItem;

        }
        private void RemoveLineStation_button(object sender, RoutedEventArgs e)
        {


            if (sender != null && sender is Button btn)
                CurrentLineStation = (BO.LineStation)btn.DataContext;

            
             bl2.RemoveLineStation(Line, CurrentLineStation.Station);
                //Line = bl2.GetLine(Line.Id);
            IEnumerable<BO.LineStation> ListLineStation = bl2.GetAllLinesStation(Line.Id);
            if (ListLineStation.Any())
            {
                ListLinesStation.ItemsSource = bl2.GetAllLinesStation(Line.Id);
                ListLinesStation.Items.Refresh();
            }
            else {
                Window3 win3 = new Window3();
                Application.Current.MainWindow = win3;

                win3.Show();
                this.Close();
            }
            
            

        }

        private void UpdateLineStation_button(object sender, RoutedEventArgs e)
        {
            if (sender != null && sender is Button btn)
                CurrentLineStation = (BO.LineStation)btn.DataContext;

            Window5 win5 = new Window5();

          
            win5.CurrentLineStation = CurrentLineStation;
            win5.CurrentLine = Line;
            win5.Init();
           
          
            win5.ShowDialog();
            this.Close();
            ListLinesStation.ItemsSource = bl2.GetAllLinesStation(Line.Id);
            ListLinesStation.Items.Refresh();

            Window3 win3 = new Window3();
            Application.Current.MainWindow = win3;
            
            win3.Show();
            
        }

        private void UpdateCodeLine_button(object sender, RoutedEventArgs e)
        {
            Window6 win6 = new Window6();
            win6.CurrentLine = Line;
            win6.Init();
            win6.ShowDialog();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window3 win3 = new Window3();
            Application.Current.MainWindow = win3;
           
            win3.Show();
            this.Close();
        }

        private void AddStation_button(object sender, RoutedEventArgs e)
        {
            Window7 win7 = new Window7();
            win7.CurrentLine = Line;
            win7.Init();
            win7.ShowDialog();
            this.Close();

        }
    }
}
