using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace dotNet5781_03B_9106_7647
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {

        public Window2()
        {

            InitializeComponent();

            this.DataContext = this;

        }
        public BUS MyData2 { get; set; }
        public ListView mylistbus;




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MyData2.Gasoline == 1200)
                MessageBox.Show("You already have 1200 km of gasoline");
            else
                MyData2.Refueling();
            mylistbus.Items.Refresh();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            MyData2.DoTreatment();
            mylistbus.Items.Refresh();
        }
    }
}