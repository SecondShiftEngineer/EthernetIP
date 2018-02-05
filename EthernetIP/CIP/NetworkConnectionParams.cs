using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    public static class NetworkConnectionParams
    {
        public const ushort Redundant = (1 << 15);
        public const ushort Owned = 0;
        public const ushort Type0 = 0;
        public const ushort MultiCast = (1 << 13);
        public const ushort Point2Point = (2 << 13);
        public const ushort LowPriority = 0;
        public const ushort HighPriority = (1 << 10);
        public const ushort ScheduledPriority = (2 << 10);
        public const ushort Urgent = (3 << 10);
        public const ushort Fixed = 0;
        public const ushort Variable = (1 << 9);
        public const byte CM_Trig_Cyclic = 0;
        public const byte CM_Trig_Change = (1 << 4);
        public const byte CM_Trig_App = (2 << 4);
        public const byte CM_Transp_IsServer = 0x80;
    }
}
