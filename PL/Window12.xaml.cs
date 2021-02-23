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
            
            //double longitude, latitude;
            try
            {
                
              
                if (string.IsNullOrEmpty((TbCode.Text)) || (string.IsNullOrEmpty(TbName.Text)) || (string.IsNullOrEmpty(TbLatitude.Text) ) || (string.IsNullOrEmpty(TbLongitude.Text) ) || (string.IsNullOrEmpty(TbAddress.Text)))
                    throw new BO.BadInputException("Bad Input");
                bl10.AddStation(Int32.Parse(TbCode.Text), TbName.Text, double.Parse(TbLongitude.Text), double.Parse(TbLatitude.Text), TbAddress.Text, (BO.Areas)CbArea.SelectedItem);


            }
            catch (BO.BadInputException ex)
            {
                MessageBox.Show("Not All the properties are filled");
            }
         
            
              
            catch (BO.BadStationCodeException ex)
            {
                MessageBox.Show("Bad Code Station");
            }
            this.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window3 win3 = new Window3();
            Application.Current.MainWindow = win3;
            win3.Show();
            this.Close();
        }
    }
}
