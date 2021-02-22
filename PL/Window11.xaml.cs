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
    /// Interaction logic for Window11.xaml
    /// </summary>
    public partial class Window11 : Window
    {
        static IBL bl9 = BLFactory.GetBL("1");
        public BO.Station CurrentStation;
        public Window11()
        {
            InitializeComponent();
        }
       

        private void Button_ClickEnter(object sender, RoutedEventArgs e)
        {
           
            string name = TbName.Text;
            string address = TbAddress.Text;
            try
            {
                bl9.UpdateStation(CurrentStation, name, address);
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
