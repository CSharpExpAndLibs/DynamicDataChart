using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicScatter
{
    public class DataModel2
    {
        public double X { get; set; }
        public double Y { get; set; }
    }


    public class DataCollection2 : ObservableCollection<DataModel2>
    {
        public int DataNum { get; set; }


        // period = msec order
        public DataCollection2(long period)
        {
            DataNum = 20;

            // データが周期間隔で昇順に並ぶようにXを設定
            for (int i = 0; i < DataNum; i++)
                Add(new DataModel2() { X = -period * (DataNum - 1 - i), Y = 0.0 });
        }

        public void Update(DataModel2 data)
        {
            RemoveAt(0);
            Add(data);
        }
    }
}
