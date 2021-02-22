using GoogleApi.Entities.Maps.Geocoding;
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
using System.Device.Location;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using BLAPI;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window12.xaml
    /// </summary>
    public partial class Window12 : Window
    {
        static IBL bl10 = BLFactory.GetBL("1");
        public Window12()
        {
            InitializeComponent();
        }
        public void Init()
        {
            List<BO.Areas> ListAreas = bl10.GetAreas();
            CbArea.ItemsSource = ListAreas;

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Button_ClickEnter(object sender, RoutedEventArgs e)
        {
            int code = Int32.Parse(TbCode.Text);
            //if (!Regex.Match(TbAddress.Text, @"^[0-200]+\s+([a-zA-Z]+|[a-zA-Z]+\s[a-zA-Z]+)$").Success)
            //{
            //    // address was incorrect  
            //    MessageBox.Show("Invalid address", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            //    TbAddress.Focus();
            //    return;
            //}
  
            double longitude = double.Parse(TbLongitude.Text);
            double latitude = double.Parse(TbLatitude.Text);
            try
            {
                bl10.AddStation(code, TbName.Text, longitude, latitude, TbAddress.Text, (BO.Areas)CbArea.SelectedItem);
            }
            catch (BO.BadStationCodeException ex)
            {
                MessageBox.Show("Bad Code Station");
            }
            this.Close();

        }

       
    }
}
