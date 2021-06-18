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
        NumericXAxis xAxis;
        int x;
        ScatterLineSeries[] seriesArray = new ScatterLineSeries[10];
        DataCollection2[] dataArray2 = new DataCollection2[10];
        Random rand = new Random();
        long originTime;

        public MainWindow()
        {
            InitializeComponent();
            InitTimeGraph();

        }

        private void InitTimeGraph()
        {
            long period = 500; // 500msec

            for (int i = 0; i < dataArray2.Length; i++)
            {
                dataArray2[i] = new DataCollection2(period);
            }

            xAxis = new NumericXAxis();
            xAxis.MinimumValue = dataArray2[0][0].X;
            xAxis.MaximumValue = 0;

            var yAxis = new NumericYAxis();
            yAxis.MinimumValue = 0;
            yAxis.MaximumValue = 1024;

            for (int i = 0; i < seriesArray.Length; i++)
            {
                seriesArray[i] = new ScatterLineSeries();
                seriesArray[i].XAxis = xAxis;
                seriesArray[i].YAxis = yAxis;
                seriesArray[i].XMemberPath = "X";
                seriesArray[i].YMemberPath = "Y";
                seriesArray[i].ItemsSource = dataArray2[i];
            }

            ScatterChart.Axes.Add(xAxis);
            ScatterChart.Axes.Add(yAxis);
            for (int i = 0; i < seriesArray.Length; i++)
                ScatterChart.Series.Add(seriesArray[i]);

            aTimer = new Timer(period);
            aTimer.Elapsed += ATimer_Elapsed2;
            aTimer.Enabled = true;

            // 現在時刻を原点に設定
            //   動的に設定するのであれば初っ端データを取得した時に
            //   設定すれば良い
            originTime = DateTime.Now.Ticks;

        }

        delegate void timerHandler(object sender, ElapsedEventArgs e);


        private void ATimer_Elapsed2(object sender, ElapsedEventArgs e)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new timerHandler(ATimer_Elapsed2),
                    new object[] { sender, e });
                return;
            }

            long now = DateTime.Now.Ticks;
            for (int i = 0; i < seriesArray.Length; i++)
            {
                double y = 512 + (rand.Next() % 256 - 128);
                double t = (now - originTime) / 10000; // msec order
                dataArray2[i].Update(new DataModel2() { X = t, Y = y });
            }

            // 全ての凡例のXが等しいと看做せるならば、これでOK
            xAxis.MinimumValue = dataArray2[0][0].X;
            xAxis.MaximumValue = dataArray2[0][dataArray2[0].Count - 1].X;
        }
    }
}
