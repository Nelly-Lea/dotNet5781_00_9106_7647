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
    /// Interaction logic for Window17.xaml
    /// </summary>
    public partial class Window17 : Window
    
    {
        static IBL bl = BLFactory.GetBL("1");
        public Window17()
        {
            InitializeComponent();
            CbArea.ItemsSource = bl.GetAreas();

        }

        private void CbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListLines.ItemsSource = bl.ShowLineInArea((BO.Areas)CbArea.SelectedItem);
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
