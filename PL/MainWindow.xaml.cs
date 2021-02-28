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
using System.Windows.Navigation;
//using System.Windows.Shapes;
using BLAPI;
using DL;
//using DLAPI;
using DO;
using DS;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl;
        public MainWindow()
            
        {
            InitializeComponent();

            XMLTools.SaveListToXMLSerializer<Station>(DS.DataSource.ListStations, @"StationXml1.xml");
            XMLTools.SaveListToXMLSerializer<Line>(DS.DataSource.ListLines, @"LineXml1.xml");
            XMLTools.SaveListToXMLSerializer<LineStation>(DS.DataSource.ListLineStations, @"LineStationXml1.xml");
            XMLTools.SaveListToXMLSerializer<AdjacentStations>(DS.DataSource.ListAdjacentStations, @"AdjacentStationXml1.xml");
            XMLTools.SaveListToXMLSerializer<User>(DS.DataSource.ListUsers, @"UserXml1.xml");
            XMLTools.SaveListToXMLSerializer<LineTrip>(DS.DataSource.ListLineTrip, @"LineTripXml1.xml");

        }

      
        private void btnGO_Click(object sender, RoutedEventArgs e)
        {

            if (DirectorWorker.IsChecked == true)
            {
                Window2 win2 = new Window2();
                win2.ShowDialog();
            }
            else
            {
                if (Passenger.IsChecked == true)
                {

                    Window1 win1 = new Window1();
                    win1.ShowDialog();


                }

            }
        }
    }
}
