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
    /// Interaction logic for Window10.xaml
    /// </summary>
    public partial class Window10 : Window
    {
        static IBL bl8 = BLFactory.GetBL("1");
       public  BO.Station CurrentStation;
        public int index;
        public Window10()
        {
            InitializeComponent();
        }
        public void Init()
        {
            ListBoxLineNumber.ItemsSource = bl8.GetAllLineInStation(index);
            TbStationName.Text = CurrentStation.Name;
            ListBoxLastStation.ItemsSource = bl8.GetAllLastStationInLine(index);
        }

        private void Button_Return(object sender, RoutedEventArgs e)
        {
            Window3 win3 = new Window3();
            Application.Current.MainWindow = win3;
            win3.Show();
            this.Close();
        }
    }
}
