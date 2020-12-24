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
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace dotNet5781_03B_9106_7647
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BUS current;
       
        public static List<BUS> listb = new List<BUS>();
     
        public ObservableCollection<BUS> oc { get; set; } = new ObservableCollection<BUS>(listb);
        
        public MainWindow()
        {
            
            
            InitializeComponent();
            initbus(oc);


            ListBus.ItemsSource = oc;



        }
        
        public void initbus(ObservableCollection<BUS> oc)
        {
            DateTime db = new DateTime(2017, 10, 15);
            DateTime dlt = new DateTime(2019, 11, 8);
            BUS bus1 = new BUS("11-111-11", db, dlt, 180000, 5000, 0, 1100);

            db = new DateTime(2018, 11, 15);
            dlt = new DateTime(2019, 04, 04);
            BUS bus2 = new BUS("222-22-222", db, dlt, 200000, 19950, 0, 1000);

            db = new DateTime(2018, 12, 18);
            dlt = new DateTime(2019, 05, 04);
            BUS bus3 = new BUS("333-33-333", db, dlt, 205000, 15000, 0, 100);
            db = new DateTime(2020, 01, 18);
            dlt = new DateTime(2020, 11, 14);

            BUS bus4 = new BUS("444-44-444", db, dlt, 100000, 10000, 0, 500);
            BUS bus5 = new BUS("555-55-555", db, dlt, 215000, 9000,0, 800);
            BUS bus6 = new BUS("666-66-666", db, dlt, 300000, 8000, 0, 400);
            BUS bus7 = new BUS("777-77-777", db, dlt, 80000, 5000, 0, 900);
            BUS bus8 = new BUS("888-88-888", db, dlt, 55000, 7000, 0, 1050);
            BUS bus9 = new BUS("999-99-999", db, dlt, 70000, 11000, 0, 600);
            BUS bus10 = new BUS("123-45-678", db, dlt, 65000, 3000, 0, 750);
            oc.Add(bus2); oc.Add(bus3); oc.Add(bus4); oc.Add(bus5);
            oc.Add(bus6); oc.Add(bus7); oc.Add(bus8); oc.Add(bus9); oc.Add(bus10);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 win2 = new Window1();

        


        }

      
        private void listbus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (sender != null && sender is Button btn)
                current = (BUS)btn.DataContext;
            Window1 win1 = new Window1();
            win1.MyData = current;
            win1.ShowDialog();
            ListBus.Items.Refresh();

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (sender != null && sender is Button btn)
                current = (BUS)btn.DataContext;
            if (current.Gasoline == 1200)
                MessageBox.Show("You already have 1200 km of gasoline");
            else
                current.Refueling();
            ListBus.Items.Refresh();


        }

        private void ListBus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            

            Window2 win2 = new Window2();
            win2.MyData2 = (BUS)ListBus.SelectedItem;
            win2.mylistbus=ListBus;
            win2.ShowDialog();
            



        }

      

    }
}