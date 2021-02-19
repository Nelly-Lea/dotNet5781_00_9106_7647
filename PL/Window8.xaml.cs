using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Window8.xaml
    /// </summary>
    /// 
    public partial class Window8 : Window
    {
        static IBL bl6 = BLFactory.GetBL("1");
        public Window8()
        {
            InitializeComponent();
        }
        public void Init()
        {
            List<BO.Areas> ListAreas = bl6.GetAreas();
            CbArea.ItemsSource = ListAreas;
           
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CbFirstStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int code = Int32.Parse(tbCode.Text);
            BO.Areas Area = (BO.Areas)CbArea.SelectedItem;
           BO.Station FirstStation = (BO.Station)CbFirstStation.SelectedItem;
            CbLastStation.ItemsSource = bl6.AddLineFirst(code, Area, FirstStation);


        }

        private void CbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CbFirstStation.ItemsSource = bl6.GetStationWithArea((BO.Areas)CbArea.SelectedItem);
        }

        private void CbLastStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int code = Int32.Parse(tbCode.Text);
            BO.Station LastStation = (BO.Station)CbLastStation.SelectedItem;
            BO.Station FirstStation = (BO.Station)CbFirstStation.SelectedItem;
            bl6.AddLine(code, FirstStation, LastStation);
            Window3 win3 = new Window3();
            win3.ListLines.ItemsSource=bl6.GetAllLines();
            win3.Show();
            this.Close();
        }
    }
}
