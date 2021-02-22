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
    /// Interaction logic for Window5.xaml
    /// </summary>
    public partial class Window5 : Window
    {
        static IBL bl3 = BLFactory.GetBL("1");
        public  BO.LineStation CurrentLineStation;
        public BO.Line CurrentLine;
        public BO.Station CurrentStation;
      

        public Window5()
        {
            InitializeComponent();
        }
          
        public void Init()
        {
            TbLine.DataContext = CurrentLine;
            TbLine.Text = CurrentLine.Code.ToString();
            TbArea.DataContext = CurrentLine;
            TbArea.Text = CurrentLine.Area.ToString();
            List<BO.Station> ListStationsArea = bl3.ShowStationArea(CurrentLine).ToList();
            ListLinesStationArea.ItemsSource = ListStationsArea;
          

        }

        private void MouseDoubleClick_UpdateLineStation(object sender,MouseButtonEventArgs e )
        {
           
                CurrentStation = (BO.Station) ListLinesStationArea.SelectedItem;
                
            try
            {
                bl3.UpdateLine(CurrentLine, CurrentLineStation, CurrentStation);
            }
            catch (BO.BadLineIdException ex)
            {
                MessageBox.Show("Bad Code Line");

            }

            this.Close();
          
            
        }
    }
}
