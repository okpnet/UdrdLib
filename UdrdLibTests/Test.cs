using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace UdrdLibTests
{
    public class Test : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int TestId { get; set; }
        public string Name { get; set; }

        public DateTime ToDay { get; }= DateTime.Now;

        public ICollection<Test> Children { get; set; }=new ObservableCollection<Test>();
        public override string ToString()
        {
            return $"Id={TestId} Name={Name} ToDay={ToDay} ChildrenCnt={Children.Count} Children...\n    {string.Join("\n    ",Children.Select(t=>t.ToString()))}";
        }
    }
}
