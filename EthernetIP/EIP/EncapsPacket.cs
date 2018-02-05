using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.EIP
{
    /// <summary>
    /// Encapsulated Packet
    /// </summary>
    public class EncapsPacket : IPackable, IExpandable
    {

        /// <summary>
        /// Encapsulation Command, see <c cref="EIPNET.EncapsCommand"/>
        /// </summary>
        public ushort Command { get; set; }
        /// <summary>
        /// Length of the data portion in bytes
        /// </summary>
        public ushort Length { get; set; }
        /// <summary>
        /// Session handle
        /// </summary>
        public uint SessionHandle { get; set; }
        /// <summary>
        /// Status Code, see <c cref="EIPNET.EncapsStatusCode"/>
        /// </summary>
        public uint Status { get; set; }

        private byte[] _senderContext = new byte[8];
        /// <summary>
        /// Sender Context
        /// </summary>
        /// <remarks>Information only pertinent to the sender of the encaps command. Must be 8 bytes.</remarks>
        public byte[] SenderContext
        {
            get { return _senderContext; }
            set
            {
                if (value == null)
                    _senderContext = new byte[8];

                if (value.Length >= 8)
                {
                    _senderContext = new byte[8];
                    Array.Copy(value, _senderContext, 8);
                }

                if (value.Length < 8)
                {
                    _senderContext = new byte[8];
                    Array.Copy(value, _senderContext, value.Length);
                }
            }
        }
        /// <summary>
        /// Options Flags
        /// </summary>
        public uint OptionsFlags { get; set; }
        /// <summary>
        /// Encapsulated data portion of the message
        /// </summary>
        /// <remarks>Required only for certain messages</remarks>
        public byte[] EncapsData { get; set; }

        /// <summary>
        /// Packs the Encapsulated Packet into an array of bytes
        /// </summary>
        /// <returns>Array of bytes</returns>
        /// <exception cref="ArgumentOutOfRangeException">EncapsData member is more than 65,511 bytes long.</exception>
        public byte[] Pack()
        {
            byte[] retVal = new byte[24 + (EncapsData == null ? 0 : EncapsData.Length)];
            //Fix the Length value

            if (EncapsData != null && EncapsData.Length > 65511)
                throw new ArgumentOutOfRangeException("EncapsData cannot be more than 65,511 bytes long");

            Length = (ushort)(EncapsData == null ? 0 : EncapsData.Length);

            Buffer.BlockCopy(BitConverter.GetBytes(Command), 0, retVal, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(Length), 0, retVal, 2, 2);
            Buffer.BlockCopy(BitConverter.GetBytes(SessionHandle), 0, retVal, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(Status), 0, retVal, 8, 4);
            Buffer.BlockCopy(_senderContext, 0, retVal, 12, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(OptionsFlags), 0, retVal, 20, 4);

            if (EncapsData != null && EncapsData.Length > 0)
                Buffer.BlockCopy(EncapsData, 0, retVal, 24, EncapsData.Length);

            return retVal;
        }

        /// <summary>
        /// Expands the DataArray into this EncapsPacket
        /// </summary>
        /// <param name="DataArray">Data array that holds the encaps packet</param>
        /// <param name="Offset">First byte of the encaps packet</param>
        /// <param name="NewOffset">New offset in the array where the next packet begins</param>
        /// <exception cref="IndexOutOfRangeException">Not enough data in the DataArray to expand the packet.</exception>
        public void Expand(byte[] DataArray, int Offset, out int NewOffset)
        {
            NewOffset = Offset;

            Command = BitConverter.ToUInt16(DataArray, Offset);
            Length = BitConverter.ToUInt16(DataArray, Offset + 2);
            SessionHandle = BitConverter.ToUInt32(DataArray, Offset + 4);
            Status = BitConverter.ToUInt32(DataArray, Offset + 8);
            _senderContext = new byte[8];
            Buffer.BlockCopy(DataArray, Offset + 12, _senderContext, 0, 8);
            OptionsFlags = BitConverter.ToUInt32(DataArray, Offset + 20);

            if (DataArray.Length < Offset + 24 + Length)
                throw new IndexOutOfRangeException("Not enough data in the DataArray for the encapsulated packet");

            if (Length > 0)
            {
                byte[] temp = new byte[Length];
                Buffer.BlockCopy(DataArray, 24, temp, 0, Length);
                EncapsData = temp;
            }

            NewOffset = Offset + 24 + Length;
        }

    }
}
