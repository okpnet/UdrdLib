using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdrdLib
{
    public enum OperateType:byte
    {
        Set=0x1,
        CollectionAdd=0x1,
        CollectionRemove=0x2,
    }
}
