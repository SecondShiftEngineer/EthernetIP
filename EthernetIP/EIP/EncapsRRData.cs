using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.EIP
{
    public class EncapsRRData : IPackable, IExpandable
    {
        /// <summary>
        /// Interface Handle, shall be 0 for CIP
        /// </summary>
        public uint InterfaceHandle { get; set; }
        /// <summary>
        /// Operation Timeout
        /// </summary>
        public ushort Timeout { get; set; }
        /// <summary>
        /// Common Packet Format
        /// </summary>
        public CommonPacket CPF { get; set; }

        /// <summary>
        /// Packs the EncapsRRData into a byte array
        /// </summary>
        /// <returns>Byte array that represents the EncapsRRData</returns>
        public byte[] Pack()
        {
            List<byte> retVal = new List<byte>();

            retVal.AddRange(BitConverter.GetBytes(InterfaceHandle));
            retVal.AddRange(BitConverter.GetBytes(Timeout));
            retVal.AddRange(CPF.Pack());

            return retVal.ToArray();
        }

        /// <summary>
        /// Expands the DataArray into this EncapsRRData object
        /// </summary>
        /// <param name="DataArray">Source array</param>
        /// <param name="Offset">Offset where the EncapsRRData object starts</param>
        /// <param name="NewOffset">Offset where the EncapsRRData object ends</param>
        public void Expand(byte[] DataArray, int Offset, out int NewOffset)
        {
            InterfaceHandle = BitConverter.ToUInt32(DataArray, Offset);
            Offset += 4;
            Timeout = BitConverter.ToUInt16(DataArray, Offset);
            Offset += 2;
            CPF = new CommonPacket();
            CPF.Expand(DataArray, Offset, out Offset);

            NewOffset = Offset;
        }
    }
}
