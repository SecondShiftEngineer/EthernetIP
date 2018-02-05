using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    /// <summary>
    /// Connection Parameters for Forward Open requests
    /// </summary>
    public struct ConnectionParameters
    {
        /// <summary>
        /// Used to calculate request timeout information
        /// </summary>
        public byte PriorityAndTick;
        /// <summary>
        /// Used to calculate request timeout information
        /// </summary>
        public byte ConnectionTimeoutTicks;
        /// <summary>
        /// Network connection ID to be used for the local link, originator to target.
        /// </summary>
        public uint O2T_CID;
        /// <summary>
        /// Network connection ID to be used for the local link, target to originator.
        /// </summary>
        public uint T2O_CID;
        /// <summary>
        /// Unique 16-bit value selected by the connection manager at the originator
        /// of the connection.
        /// </summary>
        /// <remarks>
        /// The originator shall make sure that the 16-bit value is unique for the device. There
        /// shall be no other significance placed on the number by any other node in the connection
        /// path. The connection serial numbers shall be unique but do not have to be sequential.
        /// For example, an operator interface may have a large number of connections open at the
        /// same time, each with a unique number. The same values could be repeated at other
        /// operator interface stations. A possible implementation woudl be to have a connection
        /// list which points to the descriptor for each connection, and the connection serial
        /// number could be the index into the table.
        /// </remarks>
        public ushort ConnectionSerialNumber;
        /// <summary>
        /// The vendor number shall be a unique number assigned to the various vendors of products.
        /// This is obtained from the ODVA.org website.
        /// </summary>
        public ushort VendorID;
        /// <summary>
        /// A 32 bit value that is assigned to the device at the time of manufacture.
        /// </summary>
        public uint OriginatorSerialNumber;
        /// <summary>
        /// Originator to target actual packet interval, in microseconds
        /// </summary>
        public uint O2T_API;
        /// <summary>
        /// Target to originator actual packet interval, in microseconds
        /// </summary>
        public uint T2O_API;
    }
}
