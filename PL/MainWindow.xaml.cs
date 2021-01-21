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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLAPI;
using DLAPI;


namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl;
        public MainWindow()
        {
            InitializeComponent();
            BL.BLImp b1 = new BL.BLImp();
            //bl = BLFactory.GetBL("1");
            IDL d = DLFactory.GetDL();
          var v =   d.GetAllLines();

            DO.Line l = d.GetLine(1);
            BO.Line lineBO = new BO.Line();
            lineBO.Area = (BO.Areas)l.Area;
            lineBO.Code = l.Code;
            lineBO.FirstStation = l.FirstStation;
            lineBO.Id = l.Id;
            lineBO.LastStation = l.LastStation;
      

            BO.ShowStationsLine s = b1.ShowStations(lineBO);
        }
    }
}
