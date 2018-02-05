using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    public abstract class ForwardCloseReply
    {
        public ushort ConnectionSerialNumber { get; internal set; }
        public ushort OriginatorVendorId { get; internal set; }
        public uint OriginatorSerialNumber { get; internal set; }

        
    }
}
