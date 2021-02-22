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
    /// Interaction logic for Window6.xaml
    /// </summary>
    public partial class Window6 : Window
    {
        static IBL bl4 = BLFactory.GetBL("1");
        public BO.Line CurrentLine = new BO.Line();
        public Window6()
        {
            InitializeComponent();
        }
        public void Init()
        {
            TbLineNumber.DataContext = CurrentLine;
            TbLineNumber.Text = CurrentLine.Code.ToString();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int code = Int32.Parse(tbNewLineNumber.Text);
           
            try
            {
                bl4.UpdateLineCode(CurrentLine, code);
            }
            catch (BO.BadLineIdException ex)
            {
                MessageBox.Show("Bad Code Line");

            }


            // this.Close();

            Window3 win3 = new Window3();
            Application.Current.MainWindow = win3;
            win3.ListLines.ItemsSource = bl4.GetAllLines();
            win3.Show();
            this.Close();

        }

        private void ButtonReturn_Click(object sender, RoutedEventArgs e)
        {
            Window3 win3 = new Window3();
            Application.Current.MainWindow = win3;
            win3.Show();
            this.Close();
        }
    }
}
