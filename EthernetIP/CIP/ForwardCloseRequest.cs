using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    /// <summary>
    /// Foward Close Request
    /// </summary>
    public class ForwardCloseRequest : IPackable
    {
        /// <summary>
        /// Used to calculate the request timeout information
        /// </summary>
        /// <remarks>
        /// Bits 7,6,5 are reserved. Bit 4 is priority (0 = Normal, 1 = Reserved). 
        /// Bits 3-0 are the Tick Time. The actual timeout value is 2^PriorityTimeTick * TimeoutTicks.
        /// </remarks>
        public byte PriorityTimeTick { get; set; }
        /// <summary>
        /// Used to calculate the request timeout information
        /// </summary>
        /// <remarks>
        /// The actual timeout value is 2^PriorityTimeTick * TimeoutTicks.
        /// </remarks>
        public byte TimeoutTicks { get; set; }
        /// <summary>
        /// Value assigned by the connection manager when a
        /// connection is opened. The value allows other nodes
        /// to obtain data from the connection manager.
        /// </summary>
        public ushort ConnectionSerialNumber { get; set; }
        /// <summary>
        /// Vendor Id Assigned by the ODVA
        /// </summary>
        public ushort VendorId { get; set; }
        /// <summary>
        /// Unique number assigned to the device at the time of
        /// manufacture. The number must be unique for all devices
        /// manufactured by the same vendor.
        /// </summary>
        public uint OriginatorSerialNumber { get; set; }
        /// <summary>
        /// Size of the path in 16 bit words.
        /// </summary>
        public byte PathSize { get; set; }
        /// <summary>
        /// Reserved byte
        /// </summary>
        public byte Reserved { get { return 0; } }
        /// <summary>
        /// Padded path, must be an even number of bytes
        /// </summary>
        public byte[] Path { get; set; }

        /// <summary>
        /// Packs the ForwardCloseRequest into a byte array
        /// </summary>
        /// <returns>Byte array representing the ForwardCloseRequest</returns>
        public byte[] Pack()
        {
            if (Path == null)
                throw new NullReferenceException("Path cannot be null");

            PathSize = (byte)(Path.Length / 2 + (Path.Length % 2));
            byte[] retVal = new byte[14 + PathSize * 2];

            retVal[0] = PriorityTimeTick;
            retVal[1] = TimeoutTicks;
            Buffer.BlockCopy(BitConverter.GetBytes(ConnectionSerialNumber), 0, retVal, 2, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(VendorId), 0, retVal, 4, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(OriginatorSerialNumber), 0, retVal, 6, 4);
            retVal[10] = PathSize;
            retVal[11] = 0;
            Buffer.BlockCopy(Path, 0, retVal, 12, Path.Length);

            return retVal;
        }
    }
}
