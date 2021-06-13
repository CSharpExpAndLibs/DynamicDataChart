using Infragistics.Controls.Charts;
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
using System.Timers;

namespace DynamicScatter
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer aTimer = null;
        //DataCollection data;
        NumericXAxis xAxis;
        int x;
        ScatterLineSeries[] seriesArray = new ScatterLineSeries[240];
        DataCollection[] dataArray = new DataCollection[240];
        Random rand = new Random();

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < dataArray.Length; i++)
            {
                dataArray[i] = new DataCollection();
            }
            x = dataArray[0].DataNum - 1;

            //data = new DataCollection();
            xAxis = new NumericXAxis();
            xAxis.MinimumValue = dataArray[0][0].X;
            xAxis.MaximumValue = dataArray[0][dataArray[0].Count-1].X;
            var yAxis = new NumericYAxis();
            yAxis.MinimumValue = 0;
            yAxis.MaximumValue = 1.0;
            
            for (int i = 0; i < seriesArray.Length; i++)
            {
                seriesArray[i] = new ScatterLineSeries();
                seriesArray[i].XAxis = xAxis;
                seriesArray[i].YAxis = yAxis;
                seriesArray[i].XMemberPath = "X";
                seriesArray[i].YMemberPath = "Y";
                seriesArray[i].ItemsSource = dataArray[i];
            }

#if false
            var series = new ScatterLineSeries();
            series.XAxis = xAxis;
            series.YAxis = yAxis;
            series.XMemberPath = "X";
            series.YMemberPath = "Y";
            series.ItemsSource = data;
            series.TrendLineType = TrendLineType.None;
#endif

            ScatterChart.Axes.Add(xAxis);
            ScatterChart.Axes.Add(yAxis);
            for (int i = 0; i < seriesArray.Length; i++)
                ScatterChart.Series.Add(seriesArray[i]);

            aTimer = new Timer(500);
            aTimer.Elapsed += ATimer_Elapsed;
            aTimer.Enabled = true;
        }

        delegate void timerHandler(object sender, ElapsedEventArgs e);

        private void ATimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new timerHandler(ATimer_Elapsed),
                    new object[] { sender, e });
                return;
            }
            for (int i = 0; i < seriesArray.Length; i++)
            {
                double y = rand.NextDouble();
                dataArray[i].Update(new DataModel() { X = x, Y = y });
            }
            x++;
            xAxis.MinimumValue = dataArray[0][0].X;
            xAxis.MaximumValue = dataArray[0][dataArray[0].Count - 1].X;
        }
    }
}
