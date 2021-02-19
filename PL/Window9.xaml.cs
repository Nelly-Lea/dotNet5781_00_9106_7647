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
    /// Interaction logic for Window9.xaml
    /// </summary>
    public partial class Window9 : Window
    {
        static IBL bl7 = BLFactory.GetBL("1");
        public BO.Line CurrentLine;
        public Window9()
        {
            InitializeComponent();
        }
        public void Init()
        {
            ListViewStationLine.ItemsSource = bl7.ShowStations(CurrentLine).ListStat;
            ListViewDist.ItemsSource = bl7.ShowStations(CurrentLine).ListDistances;
            ListViewTime.ItemsSource = bl7.ShowStations(CurrentLine).ListTimes;
            TbLineNumber.DataContext = CurrentLine;
            TbLineNumber.Text = CurrentLine.Code.ToString();
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
