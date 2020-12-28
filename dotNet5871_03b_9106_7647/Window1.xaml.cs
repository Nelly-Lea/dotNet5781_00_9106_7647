
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
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;


namespace dotNet5781_03B_9106_7647
{

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        System.Windows.Threading.DispatcherTimer DispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public Window1()
        {
            InitializeComponent();
        }
        public BUS MyData { get; set; }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int k = Int32.Parse(txtlenght.Text);
            if (k > 1200)
            {
                MessageBox.Show("you can't travel more than 1200 km");

            }
            else
            {
                if (MyData.VerifBusCanTravel(k))
                {

                    this.Close();
                }
                else
                {
                    if (MyData.Gasoline < k)
                        MessageBox.Show("Impossible to travel you need to refuel");
                    if (MyData.Km + k > 20000)
                        MessageBox.Show("Impossible to travel, you exceeded the authorized number of kilometers, you need to do a treatment ");
                    if ((DateTime.Now - MyData.Datetr).Days > 365)
                        MessageBox.Show("Impossible to travel, its been over a year since your last treatment, you need to do it");
                    if (MyData.statusp != 0)
                        MessageBox.Show("the bus is not ready to travel");

                }
            }

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

        }
    }

}