using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdrdLib
{
    public class PropertyChangedEventHasArgs : PropertyChangedEventArgs
    {
        public object BeforeValue { get; set; }
        public object AfterValue { get; set; }

        public PropertyChangedEventHasArgs(string? propertyName,object beforeValue,object afterValue) : base(propertyName)
        {
            BeforeValue = beforeValue;
            AfterValue = afterValue;
        }
    }
}
