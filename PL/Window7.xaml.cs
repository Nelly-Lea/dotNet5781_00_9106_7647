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
    /// Interaction logic for Window7.xaml
    /// </summary>
    public partial class Window7 : Window
    {
        static IBL bl5 = BLFactory.GetBL("1");
        public BO.Station CurrentStation;
        public BO.Line CurrentLine;
       

        public Window7()
        {
            InitializeComponent();
        }
        public void Init()
        {
            TbLine.DataContext = CurrentLine;
            TbLine.Text = CurrentLine.Code.ToString();
            TbArea.DataContext = CurrentLine;
            TbArea.Text = CurrentLine.Area.ToString();
            List<BO.Station> ListStationsArea = bl5.ShowStationArea(CurrentLine).ToList();
            ListLinesStationArea.ItemsSource = ListStationsArea;
          

        }

        private void MouseDoubleClick_AddStation(object sender, MouseButtonEventArgs e)
        {

            CurrentStation = (BO.Station)ListLinesStationArea.SelectedItem;
            CurrentLine=bl5.AddLineStation(CurrentLine, CurrentStation);

            Window4 win4 = new Window4();
            Application.Current.MainWindow = win4;
            win4.ListLinesStation.ItemsSource = bl5.GetAllLinesStation(CurrentLine.Id);
            win4.TbLineNumber.DataContext = CurrentLine;
            win4.TbLineNumber.Text = CurrentLine.Code.ToString();
            win4.TbArea.DataContext = CurrentLine;
            win4.TbArea.Text = CurrentLine.Area.ToString();
            win4.Line = CurrentLine;
            win4.Show();
            this.Close();



        }
    }
}
