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
    /// Interaction logic for Window15.xaml
    /// </summary>
    public partial class Window15 : Window
    {
        public BO.Line CurrentLine;
        public BO.LineTrip CurrentLineTrip;
        static IBL bl = BLFactory.GetBL("1");
        public Window15()
        {
            InitializeComponent();
            CbLineNumbers.ItemsSource = bl.GetAllLines();
           
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentLine = (BO.Line)CbLineNumbers.SelectedItem;
            LvSchedule.ItemsSource = bl.ShowLineTrips(CurrentLine);
        }
        private void Button_Click_RemoveLineTrip(object sender, RoutedEventArgs e)
        {
            if (sender != null && sender is Button btn)
                CurrentLineTrip = (BO.LineTrip)btn.DataContext;
            bl.RemoveLineTrip(CurrentLineTrip);
            LvSchedule.ItemsSource = bl.GetAllLineTrips(CurrentLine);
            LvSchedule.Items.Refresh();



        }

        private void Button_ClickAddLineTrip(object sender, RoutedEventArgs e)
        {
            Window16 win16 = new Window16();
            win16.Show();
            LvSchedule.ItemsSource = bl.GetAllLineTrips(CurrentLine);
            LvSchedule.Items.Refresh();
            
        }
    }
}
