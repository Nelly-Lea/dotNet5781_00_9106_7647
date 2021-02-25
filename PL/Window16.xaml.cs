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
    /// Interaction logic for Window16.xaml
    /// </summary>
    public partial class Window16 : Window
    {
        static IBL bl = BLFactory.GetBL("1");
        public BO.Line CurrentLine;

        public Window16()
        {
            InitializeComponent();
            CbHours.ItemsSource = bl.Listhours();
            CbMin.ItemsSource = bl.ListMinOrSec();
            CbSec.ItemsSource = bl.ListMinOrSec();
            CbLineNumbers.ItemsSource = bl.GetAllLines();

        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentLine = (BO.Line)CbLineNumbers.SelectedItem;
            TimeSpan TimeStartAt = new TimeSpan(Int32.Parse(CbHours.Text),Int32.Parse(CbMin.Text),Int32.Parse(CbSec.Text));
            bl.AddLineTrip(CurrentLine, TimeStartAt);
            this.Close();
        }

      
    }
}
