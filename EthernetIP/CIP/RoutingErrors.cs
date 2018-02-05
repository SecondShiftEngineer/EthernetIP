using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    public enum RoutingErrors : ushort
    {
        Timeout = 0x204,
        InvalidPortID = 0x311,
        InvalidNodeAddress = 0x313,
        InvalidSegmentType = 0x315
    }
}
