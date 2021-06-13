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
        DataCollection data;
        NumericXAxis xAxis;
        int x = 100;

        public MainWindow()
        {
            InitializeComponent();

            data = new DataCollection();
            xAxis = new NumericXAxis();
            xAxis.MinimumValue = data[0].X;
            xAxis.MaximumValue = data[data.Count-1].X;
            var yAxis = new NumericYAxis();
            yAxis.MinimumValue = 0;
            yAxis.MaximumValue = 1.0;

            var series = new ScatterLineSeries();
            series.XAxis = xAxis;
            series.YAxis = yAxis;
            series.XMemberPath = "X";
            series.YMemberPath = "Y";
            series.ItemsSource = data;
            series.TrendLineType = TrendLineType.None;

            ScatterChart.Axes.Add(xAxis);
            ScatterChart.Axes.Add(yAxis);
            ScatterChart.Series.Add(series);

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
            double y = new Random().NextDouble();
            //data.Update(new DataModel() { X = x++, Y = y });
            data.Replace(new DataModel() { X = x++, Y = y });
            xAxis.MinimumValue = data[0].X;
            xAxis.MaximumValue = data[data.Count - 1].X;
        }
    }
}
