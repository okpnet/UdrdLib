using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdrdLib.Commando
{
    public interface IPropertyChangeCommand:IExecuteCommand
    {
        /// <summary>
        /// セットされるまえの値
        /// </summary>
        object? BeforeValue { get; }
        /// <summary>
        /// セットされた後の値
        /// </summary>
        object? AfterValue { get; }
    }
}
