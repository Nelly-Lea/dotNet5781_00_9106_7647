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
    /// Interaction logic for Window13.xaml
    /// </summary>
    public partial class Window13 : Window
    {
        IBL bl = BLFactory.GetBL("1");

        public string CurrentUser;
        public Window13()
        {
            InitializeComponent();
        }
         public void init()
        {
           
            TbUserName.Text = CurrentUser;
            CbStartStation.ItemsSource=bl.ShowBusStations().stations;
          
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window14 win14 = new Window14();
            win14.CurrentStationStart =(BO.Station) CbStartStation.SelectedItem;
            win14.CurrentStationFinish =(BO.Station) CbFinishStation.SelectedItem;

            try
            {
                win14.init();
                win14.Show();
            }

            catch (BO.BadStationCodeException ex)
            {
                MessageBox.Show("There is no line which travels between this 2 stations");


            }

        }

        private void CbStartStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           CbFinishStation.ItemsSource= bl.GetAllStationWithoutStartStation((BO.Station)CbStartStation.SelectedItem);
            CbFinishStation.SelectedIndex = 0;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window18 win18 = new Window18();
            win18.CurrentStation = (BO.Station)CbStartStation.SelectedItem;
            win18.init();
            win18.Show();
        }
    }
}
