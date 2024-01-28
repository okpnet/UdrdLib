using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdrdLib
{
    public enum StateType:byte
    {
        Unchange=0x0,
        AddUnchange=0x2,
        Add=0x3,
        ModifyUnchange=0x4,
        Modify=0x1,
    }
}
