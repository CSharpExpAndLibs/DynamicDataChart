using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if false
namespace DynamicScatter
{
    public class DataModel
    {
        public double X { get; set; }
        public double Y { get; set; }
    }


    public class DataCollection : ObservableCollection<DataModel>
    {
        List<DataModel> backupData = null;
        public int DataNum { get; set; }

        public DataCollection()
        {
            DataNum = 20;
            backupData = new List<DataModel>();

            for (int i = 0; i < DataNum; i++)
                Add(new DataModel() { X = i, Y = 0.0 });
            foreach (var d in this)
                backupData.Add(d);
        }

        public void Update(DataModel data)
        {
            RemoveAt(0);
            Add(data);
        }

        public void Replace(DataModel data)
        {
            backupData.RemoveAt(0);
            backupData.Add(data);
            Clear();
            foreach (var d in backupData)
                Add(d);
        }
    }
}
#endif // false