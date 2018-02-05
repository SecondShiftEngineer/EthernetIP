using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    /// <summary>
    /// Search Connection Data Request
    /// </summary>
    public class SearchConnectionDataRequest : IPackable
    {
        /// <summary>
        /// Connection serial number of established connection
        /// </summary>
        public ushort ConnectionSerialNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Vendor ID of the originating node
        /// </summary>
        public ushort OriginatorVendorId
        {
            get;
            set;
        }

        /// <summary>
        /// Serial number of the originating node
        /// </summary>
        public uint OriginatorSerialNumber
        {
            get;
            set;
        }
    
        /// <summary>
        /// Packs the request into a byte array
        /// </summary>
        /// <returns>Byte array representing the request</returns>
        public byte[] Pack()
        {
            byte[] retVal = new byte[8];
            Buffer.BlockCopy(BitConverter.GetBytes(ConnectionSerialNumber), 0, retVal, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(OriginatorVendorId), 0, retVal, 2, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(OriginatorSerialNumber), 0, retVal, 4, 4);

            return retVal;
        }
    }
}
