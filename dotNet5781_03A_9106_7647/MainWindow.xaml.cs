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
using dotNet5781_02_9106_7647;

namespace dotNet5781_03A_9106_7647
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = CBL1.collec.Find(x=>x.buslinenumber==index);
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.stations;
            tbArea.DataContext = currentDisplayBusLine;
            string s = currentDisplayBusLine.areap.ToString();
            tbArea.Text = s;
           
           
        }
        static public int verifthereis2busline(Collectionbusline CBL, int buslinenumber)// verify if there are already 2 buses with the same line number

        {

            int count = 0;

            foreach (var item in CBL.collec)

            {

                if (item.buslinenumber == buslinenumber)

                    count++;



            }

            if (count == 0)
                return 0;
            else
            {
                return -1;
            }

                

        }
        public static void createbusline(List<busstationline> lbs, ref Collectionbusline CBL1, Random rand)
        {
            
            int f, l, middle, buslinenumber;
            do
            {
                 f = rand.Next(0, 39);
                 l = rand.Next(0, 39);
                middle = rand.Next(0, 39);
                 buslinenumber = rand.Next(60, 80);

            }
            while((verifthereis2busline(CBL1,buslinenumber)==-1)|| (f ==l)||(f==middle)||(l==middle));
           
            busstationline b = new busstationline();
            b.busstationkey = lbs[middle].busstationkey;
            b.longitude = lbs[middle].longitude;
            b.latitude = lbs[middle].latitude;

                busstationline bsf = new busstationline();
                bsf.busstationkey = lbs[f].busstationkey;
                bsf.longitude = lbs[f].longitude;
                bsf.latitude = lbs[f].latitude;
                busstationline bsl = new busstationline();
                bsl.busstationkey = lbs[l].busstationkey;
                bsl.longitude = lbs[l].longitude;
                bsl.latitude = lbs[l].latitude;
                busline bus1 = new busline(buslinenumber,lbs[f],lbs[l],rand);
                bus1.addstation(ref bsf);
                bus1.addstation(ref b);
                bus1.addstation(ref bsl);
                CBL1.collec.Add(bus1);

          //  }
        }
        public static void createstation(ref List<busstationline> lbs,int ind, Random rand)
        {
            busstationline b = new busstationline(ind,rand);
            lbs.Add(b);
        }
        Collectionbusline CBL1 = new Collectionbusline();
        private busline currentDisplayBusLine;
        Random rand = new Random();
       
        public MainWindow()
        {
            List<busstationline> lbs = new List<busstationline>();
          
            InitializeComponent();
            for (int i = 1; i <= 40; i++)
            {
                createstation(ref lbs, i, rand);
            }
            for (int i = 0; i <10; i++)
            {
                createbusline(lbs, ref CBL1, rand);
            }

            cbCollectionbusline.ItemsSource = CBL1.collec ;
            cbCollectionbusline.DisplayMemberPath = "buslinenumber";
            cbCollectionbusline.SelectedIndex = 0;
            
            

          //  ShowBusLine(cbCollectionbusline.SelectedIndex);

           // Console.WriteLine(currentDisplayBusLine.ToString());
          //foreach(var item in currentDisplayBusLine.stations)
          //  {
          //      Console.WriteLine(item.ToString());
          //      Console.WriteLine("lenght:{0}",item.lenghtlaststation);
          //  }
         
        }

        private void cbCollectionbusline_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbCollectionbusline.SelectedValue as busline).buslinenumber);


        }

        private void tbArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            ;
        }
    }
}
