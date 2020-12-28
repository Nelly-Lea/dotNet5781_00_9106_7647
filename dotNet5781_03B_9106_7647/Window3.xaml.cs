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
using System.Collections;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace dotNet5781_03B_9106_7647
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public ListView ListBus;

        BUS bus = new BUS();

        public ObservableCollection<BUS> oc { get; set; } = new ObservableCollection<BUS>();
        int g;
        public Window3()
        {

            InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int kmb, kmt;
            g = Int32.Parse(tbfgasoline.Text);
            kmb = Int32.Parse(tbkmbegin.Text);
            kmt = Int32.Parse(tbkmfromlasttreatment.Text);
            //bus.Num = tbbusnumber.Text;
            bus.Num = tbbusnumber.Text;
            // DateTime from=dpdatetreatment.SelectedDate.Value
            bus.Dates = dpdatebegin.SelectedDate.Value;
            bus.Datetr = dpdatetreatment.SelectedDate.Value;
            bus.Gasoline = g;
            bus.KmBegin = kmb;
            bus.Km = kmt;
            bus.statusp = 0;

            if (bus.VerifInitialisationBus())
            {
                oc.Add(bus);
                ListBus.Items.Refresh();
            }
            else
                MessageBox.Show("error initialisation");
            this.Close();



        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
