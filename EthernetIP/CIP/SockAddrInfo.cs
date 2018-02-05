using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    /// <summary>
    /// Socket Address Info Item
    /// </summary>
    public class SockAddrInfo : IPackable, IExpandable 
    {

        /// <summary>
        /// Socket Family
        /// </summary>
        public short Sin_Family { get; set; }
        /// <summary>
        /// Connection (TCP/UDP) Port
        /// </summary>
        public ushort Sin_Port { get; set; }
        /// <summary>
        /// IP Address
        /// </summary>
        public uint Sin_Addr { get; set; }

        private byte[] _sin_Zero = new byte[8];
        /// <summary>
        /// Array of 8 zero's
        /// </summary>
        public byte[] Sin_Zero { get { return _sin_Zero; } }

        /// <summary>
        /// Packs the SocketAddrInfo into a byte array
        /// </summary>
        /// <returns>Array of bytes</returns>
        public byte[] Pack()
        {
            byte[] retVal = new byte[16];

            Buffer.BlockCopy(BitConverter.GetBytes(Sin_Family), 0, retVal, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(Sin_Port), 0, retVal, 2, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(Sin_Addr), 0, retVal, 4, 4);

            return retVal;
        }

        /// <summary>
        /// Expands the DataArray into a SockAddrInfo item
        /// </summary>
        /// <param name="DataArray">Data array that contains the SockAddrInfo item</param>
        /// <param name="Offset">Offset in the array where the SockAddrInfo item starts</param>
        /// <param name="NewOffset">[Out] Offset in the array where the SockAddrInfo ends</param>
        public void Expand(byte[] DataArray, int Offset, out int NewOffset)
        {
            Sin_Family = BitConverter.ToInt16(DataArray, Offset);
            Sin_Port = BitConverter.ToUInt16(DataArray, Offset + 2);
            Sin_Addr = BitConverter.ToUInt32(DataArray, Offset + 4);

            NewOffset = Offset + 16;
        }
    }
}
