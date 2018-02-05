using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.EIP
{
    public enum CommonPacketTypeId : ushort
    {
        NULL = 0x0000,
        ListIdentityResponse = 0x000C,
        ConnectionBased = 0x00A1,
        ConnectedTransportPacket = 0x00B1,
        UnconnectedMessage = 0x00B2,
        SocketAddrInfo_O2T = 0x8000,
        SocketAddrInfo_T2O = 0x8001,
        SequencedAddressItem = 0x8002
    }
}
