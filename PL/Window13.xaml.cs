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
           // TbUserName.DataContext = CurrentUser;
            TbUserName.Text = CurrentUser;
            CbStartStation.ItemsSource=bl.ShowBusStations().stations;
          
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window14 win14 = new Window14();
            win14.CurrentStationStart =(BO.Station) CbStartStation.SelectedItem;
            win14.CurrentStationFinish =(BO.Station) CbFinishStation.SelectedItem;
            win14.Show();
            win14.init();

           
           
        }

        private void CbStartStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           CbFinishStation.ItemsSource= bl.GetAllStationWithoutStartStation((BO.Station)CbStartStation.SelectedItem);
        }
    }
}
