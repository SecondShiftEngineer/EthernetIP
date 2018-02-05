using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EIPNET.CIP
{
    /// <summary>
    /// CIP Identity Item
    /// </summary>
    public class IdentityItem : IExpandable
    {
        /// <summary>
        /// Code indicating item type of CIP Identity (0x0C)
        /// </summary>
        public ushort ItemTypeCode { get; internal set; }
        /// <summary>
        /// Number of bytes in item which follows (length varies depending on ProductName string)
        /// </summary>
        public ushort ItemLength { get; internal set; }
        /// <summary>
        /// Encapsulated Protocol Version supported
        /// </summary>
        public ushort EncapsProtocolVersion { get; internal set; }
        /// <summary>
        /// Socket Address
        /// </summary>
        public SockAddrInfo SocketAddress { get; internal set; }
        /// <summary>
        /// Vendor ID assigned by ODVA
        /// </summary>
        public ushort VendorId { get; internal set; }
        /// <summary>
        /// Device type assigned by ODVA
        /// </summary>
        public ushort DeviceType { get; internal set; }
        /// <summary>
        /// Product code assigned with respect to DeviceType
        /// </summary>
        public ushort ProductCode { get; internal set; }
        /// <summary>
        /// Device Revision (Major/Minor)
        /// </summary>
        public byte[] Revision { get; internal set; }
        /// <summary>
        /// Current status of the device
        /// </summary>
        public short Status { get; internal set; }
        /// <summary>
        /// Serial number of the device
        /// </summary>
        public uint SerialNumber { get; internal set; }
        /// <summary>
        /// Human readable description of the device
        /// </summary>
        public string ProductName { get; internal set; }
        /// <summary>
        /// Current state of the device
        /// </summary>
        public byte State { get; internal set; }

        /// <summary>
        /// Expands the DataArray into an IdentityItem
        /// </summary>
        /// <param name="DataArray"></param>
        /// <param name="Offset"></param>
        /// <param name="NewOffset"></param>
        public void Expand(byte[] DataArray, int Offset, out int NewOffset)
        {
            ItemTypeCode = BitConverter.ToUInt16(DataArray, Offset);
            Offset += 2;

            ItemLength = BitConverter.ToUInt16(DataArray, Offset);
            Offset += 2;

            EncapsProtocolVersion = BitConverter.ToUInt16(DataArray, Offset);
            Offset += 2;

            SocketAddress = new SockAddrInfo();
            SocketAddress.Expand(DataArray, Offset, out Offset);
            //Offset += 1;

            VendorId = BitConverter.ToUInt16(DataArray, Offset);
            Offset += 2;

            DeviceType = BitConverter.ToUInt16(DataArray, Offset);
            Offset += 2;

            ProductCode = BitConverter.ToUInt16(DataArray, Offset);
            Offset += 2;

            Revision = new byte[2];

            Revision[0] = DataArray[Offset];
            Offset++;

            Revision[1] = DataArray[Offset];
            Offset++;

            Status = BitConverter.ToInt16(DataArray, Offset);
            Offset += 2;

            SerialNumber = BitConverter.ToUInt32(DataArray, Offset);
            Offset += 4;

            int strSize = DataArray[Offset];
            Offset += 1;

            ProductName = System.Text.ASCIIEncoding.ASCII.GetString(DataArray, Offset, strSize);
            Offset += strSize;

            State = DataArray[Offset];

            NewOffset = Offset;
        }
    }
}
