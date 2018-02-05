using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    /// <summary>
    /// Connection Manager Services
    /// </summary>
    public enum ConnectionManagerService : byte
    {
        /// <summary>
        /// Unconnected Send Request
        /// </summary>
        UnconnectedSend = 0x52,
        /// <summary>
        /// Forward Open Request
        /// </summary>
        ForwardOpen = 0x54,
        /// <summary>
        /// Forward Close Request
        /// </summary>
        ForwardClose = 0x4E,
        /// <summary>
        /// For diagnostics of a connection
        /// </summary>
        GetConnection_Data = 0x56,
        /// <summary>
        /// For diagnostics of a connection
        /// </summary>
        Search_Connection_Data = 0x57,
        /// <summary>
        /// Reserved for definition of connection opens
        /// with a size larger than 504 bytes.
        /// </summary>
        Ex_ForwardOpen = 0x59,
        /// <summary>
        /// Determine the owner of a redundant connection
        /// </summary>
        Get_Connection_Owner = 0x5A
    }
}
