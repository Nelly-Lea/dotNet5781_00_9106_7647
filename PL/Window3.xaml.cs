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


        public Window3()
        {

            InitializeComponent();
           
            var listLinesObs = new ObservableCollection<BO.Line>(listOfLines);
            ListLines.ItemsSource = listLinesObs;
           
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
            
            // win4.Line = CurrentLine1;
            List<BO.LineStation> L = new List<BO.LineStation>();
            L= bl1.GetLineStation(CurrentLine1.Id).ToList();
            win4.ListLineStations = bl1.GetLineStation(CurrentLine1.Id).ToList();
            win4.Line = CurrentLine1;
            win4.InitList();
            win4.ShowDialog();

            // win4.LineId = CurrentLine1.Id;



            //if (sender != null && sender is Button btn)
            //    current = (BUS)btn.DataContext;
            //if (current.Gasoline == 1200)
            //    MessageBox.Show("You already have 1200 km of gasoline");
            //else
            //    current.Refueling();
            //ListBus.Items.Refresh();


        }


    }
     
       
        //public List<BO.Line> ListOfLines { get => listOfLines; set => listOfLines = value; }
    
}
